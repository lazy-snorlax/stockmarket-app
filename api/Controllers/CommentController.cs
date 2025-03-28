using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentRepository commRepo, IStockRepository stockRepo, UserManager<User> userManager)
        {
            _commRepo = commRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comments = await _commRepo.GetAllAsync();
            var commentDto = comments.Select(x => x.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commRepo.GetByIdAsync(id);
            if (comment == null) { return NotFound(); }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto dto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(!await _stockRepo.StockExists(stockId)){
                return BadRequest("Stock does not exist");
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var commModel = dto.ToCommentFromCreateDTO(stockId);
            commModel.UserId = user.Id;
            await _commRepo.CreateAsync(commModel);
            return CreatedAtAction(nameof(GetById), new { id = commModel.Id }, commModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto dto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commRepo.UpdateAsync(id, dto.ToCommentFromUpdateDTO());
            if (comment == null) { return NotFound("Comment not found"); }
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var commentModel = await _commRepo.DeleteAsync(id);
            if (commentModel == null) { return NotFound("Comment does not exist"); }

            return Ok();
        }
    }
}