using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/orderdetails")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderDetailsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> CreateOrderDetail([FromBody] OrderDetail detail)
        {
            try
            {
                _context.OrderDetails.Add(detail);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrderDetail), new { id = detail.OrderDetailId }, detail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var detail = await _context.OrderDetails.FindAsync(id);
            if (detail == null)
                return NotFound();
            return detail;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderDetail(int id, [FromBody] OrderDetail detail)
        {
            if (id != detail.OrderDetailId)
                return BadRequest("OrderDetail ID mismatch");

            _context.Entry(detail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var detail = await _context.OrderDetails.FindAsync(id);
            if (detail == null)
                return NotFound();

            _context.OrderDetails.Remove(detail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
