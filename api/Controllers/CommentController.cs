using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commRepo;

        public CommentController(ICommentRepository commRepo)
        {
            _commRepo = commRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commRepo.GetAllAsync();

            var commentDto = comments.Select(x => x.ToCommentDto());

            return Ok(commentDto);
        }
    }
}