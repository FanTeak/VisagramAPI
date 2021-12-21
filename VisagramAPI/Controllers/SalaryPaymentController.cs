using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisagramAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace VisagramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryPaymentController : ControllerBase
    {
        private readonly VisagramDbContext _context;
        public SalaryPaymentController(VisagramDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryPayment>>> GetSalaryPayments()
        {
            return await _context.SalaryPayments.Include(x=>x.Staff).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryPayment>> GetSalaryPayment(long id)
        {
            var paymentDetails = await (from payment in _context.Set<SalaryPayment>()
                join detail in _context.Set<SalaryDetails>() 
                    on payment.PaymentId equals detail.SalaryPaymentId
                join offer in _context.Set<SalaryOffer>()
                    on detail.SalaryOfferId equals offer.SalaryOfferId
                where payment.PaymentId == id
                select new
                {
                    payment.PaymentId,
                    detail.SalaryDetailsId,
                    detail.SalaryOfferId,
                    detail.Quantity,
                    detail.SalaryOfferValue,
                    offer.OfferName
                }).ToListAsync();

            var salaryPayment = await (from a in _context.Set<SalaryPayment>()
                where a.PaymentId == id
                select new
                {
                    a.PaymentId,
                    a.PaymentNumber,
                    a.StaffId,
                    a.PaymentType,
                    a.Total,
                    deletedSalaryItemIds = "",
                    orderDetails = paymentDetails
                }).FirstOrDefaultAsync();

            if (salaryPayment == null)
            {
                return NotFound();
            }

            return Ok(salaryPayment);
        }

        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaryPayment(long id, SalaryPayment salaryPayment)
        {
            if (id != salaryPayment.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(salaryPayment).State = EntityState.Modified;

            foreach (SalaryDetails detail in salaryPayment.OrderDetails)
            {
                if (detail.SalaryDetailsId == 0)
                    _context.SalaryDetails.Add(detail);
                else
                    _context.Entry(detail).State = EntityState.Modified;
            }

            foreach (var i in salaryPayment.DeletedSalaryItemIds.Split(',').Where(x => x != ""))
            {
                SalaryDetails y = _context.SalaryDetails.Find(Convert.ToInt64(i));
                _context.SalaryDetails.Remove(y);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalaryPayment>> PostSalaryPayment(SalaryPayment salaryPayment)
        {
            _context.SalaryPayments.Add(salaryPayment);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetSalaryPayment", new { id = salaryPayment.PaymentId}, salaryPayment);
            return CreatedAtAction("GetSalaryPayment", new {id = salaryPayment.PaymentId}, salaryPayment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryPayment(long id)
        {
            var salaryPayment = await _context.SalaryPayments.FindAsync(id);
            if (salaryPayment == null)
            {
                return NotFound();
            }

            _context.SalaryPayments.Remove(salaryPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaryPaymentExists(long id)
        {
            return _context.SalaryPayments.Any(e => e.PaymentId == id);
        }
    }
}