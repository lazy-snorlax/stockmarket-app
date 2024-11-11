using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commRepo, IStockRepository stockRepo)
        {
            _commRepo = commRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commRepo.GetAllAsync();
            var commentDto = comments.Select(x => x.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commRepo.GetByIdAsync(id);
            if (comment == null) { return NotFound(); }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto dto) {
            // Console.WriteLine('test');
            if(!await _stockRepo.StockExists(stockId)){
                return BadRequest("Stock does not exist");
            }

            var commModel = dto.ToCommentFromCreateDTO(stockId);
            await _commRepo.CreateAsync(commModel);
            return CreatedAtAction(nameof(GetById), new { id = commModel.Id }, commModel.ToCommentDto());
        }
    }
}