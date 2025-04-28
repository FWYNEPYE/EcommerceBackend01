using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context){
            _context = context;
        }

        //Tạo mới Cate
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(
            [FromBody] CategoryRequest request)
        {
           try
           {
                var category = new Category {
                    Name = request.Name,
                    Description = request.Description
                };                  
                
                 _context.Categories.Add(category);  
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof (GetCategory), new {id = category.Id}, category);
           }
           catch (Exception ex)
           {
                return BadRequest (ex.Message);
           }
        }

      // Lấy Category theo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }


    }

    public class CategoryRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

}