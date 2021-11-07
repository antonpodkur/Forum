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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            postDto = await _postService.AddAsync(postDto);
            return Ok(postDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePost([FromBody] PostDTO postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _postService.UpdateAsync(postDto);
            return Ok(postDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
        {
            return Ok(await _postService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PostDTO>> GetPostById(string id)
        {
            return Ok(await _postService.GetByIdAsync(id));
        }

        [HttpGet("comments/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(string id)
        {
            var postDto = await _postService.GetByIdAsync(id);
            var comments = postDto.Comments.ToList();
            return Ok(comments);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePostById(string id)
        {
            await _postService.RemoveAsync(id);
            return Ok();
        }
    }
}