using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview([FromBody] ReviewRequest request)
        {
            try
            {
                var review = new Review
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Rating = request.Rating,
                    Comment = request.Comment
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/reviews/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            return review;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // PUT: api/reviews/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewRequest request)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            review.Rating    = request.Rating;
            review.Comment   = request.Comment;
            review.UserId    = request.UserId;
            review.ProductId = request.ProductId;

            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/reviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    /// <summary>
    /// DTO cho việc tạo/cập nhật Review
    /// </summary>
    public class ReviewRequest
    {
        public Guid   UserId    { get; set; }
        public int    ProductId { get; set; }
        public int    Rating    { get; set; }
        public string? Comment   { get; set; }
    }
}
