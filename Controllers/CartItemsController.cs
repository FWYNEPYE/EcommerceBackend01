using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/cartitems")]
    public class CartItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartItemController(AppDbContext context)
        {
            _context = context;
        }

        // Tạo mới CartItem
        [HttpPost]
        public async Task<ActionResult<CartItem>> CreateCartItem([FromBody] CartItemRequest request)
        {
            try
            {
                var cartItem = new CartItem
                {
                    ProductName = request.ProductName,
                    Quantity = request.Quantity,
                    Price = request.Price
                };

                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Lấy thông tin CartItem theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            return cartItem;
        }
    }

    // Lớp yêu cầu khi tạo mới CartItem
    public class CartItemRequest
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    // Lớp mô hình CartItem
    public class CartItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
