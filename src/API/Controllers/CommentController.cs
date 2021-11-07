using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstractions;
using BLL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            commentDto = await _commentService.AddAsync(commentDto);
            return Ok(commentDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentDTO commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.UpdateAsync(commentDto);
            return Ok(commentDto);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetPostComments(string postId)
        {
            var post = await _postService.GetByIdAsync(postId);
            return Ok(post.Comments);
        }
        //TODO: complete httpget/postid, httpget - return all, check 'Delete comment by id'
        //TODO: make Post load comments.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentById(string id)
        {
            await _commentService.RemoveAsync(id);
            return Ok();
        }
        
    }
}