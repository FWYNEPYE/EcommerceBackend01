using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return await _context.Payments
                .Include(p => p.Order)
                .ToListAsync();
        }

        // GET: api/payments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return NotFound();

            return payment;
        }

        // POST: api/payments
        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment([FromBody] PaymentRequest request)
        {
            var payment = new Payment
            {
                OrderId = request.OrderId,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentStatus,
                TransactionId = request.TransactionId,
                PaidAmount = request.PaidAmount,
                PaidAt = request.PaidAt ?? DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }

        // PUT: api/payments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentRequest request)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            payment.OrderId = request.OrderId;
            payment.PaymentMethod = request.PaymentMethod;
            payment.PaymentStatus = request.PaymentStatus;
            payment.TransactionId = request.TransactionId;
            payment.PaidAmount = request.PaidAmount;
            payment.PaidAt = request.PaidAt ?? payment.PaidAt;

            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/payments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    /// <summary>
    /// DTO for creating/updating Payment
    /// </summary>
    public class PaymentRequest
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string? TransactionId { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
