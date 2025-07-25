using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Comment;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;

namespace WebApplication1.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comments = await _commentRepo.GetComments();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepo.GetComment(id);
            if (commentModel == null)
            {
                return NotFound();
            }
            else
            {
                var commentDto = commentModel.ToCommentDto();
                return Ok(commentDto);
            }
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDTO commentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _stockRepo.StockExist(stockId))
            {
                return NotFound("Stock does not exist!");
            }
            else
            {
                var commentModel = commentDTO.ToCommentFromCreate(stockId);
                await _commentRepo.CreateCommentAsync(commentModel);
                return CreatedAtAction(nameof(GetComment), new { id = commentModel }, commentModel.ToCommentDto());
            }
        }

        [HttpPut("{stockId:int}/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int stockId, [FromRoute] int id, [FromBody] UpdateCommentRequestDTO commentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _stockRepo.StockExist(stockId))
            {
                return NotFound("Stock does not exist!");
            }

            var commentModel = await _commentRepo.UpdateCommentAsync(id, commentDTO.ToCommentFromUpdate());
            if (commentModel == null)
            {
                return NotFound("Comment does not exist!");
            }
            else
            {
                return Ok(commentModel.ToCommentDto());
            }
        }

        [HttpDelete("{stockId}/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int stockId, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _stockRepo.StockExist(stockId))
            {
                return NotFound("Stock does not exist!");
            }

            var commentModel = await _commentRepo.DeleteCommentAsync(id);
            if (commentModel == null){
                return NotFound("Comment does not exist");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
