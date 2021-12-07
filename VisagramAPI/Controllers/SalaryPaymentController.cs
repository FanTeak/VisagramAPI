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

        //GET: api/Game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryPayment>>> GetSalaryPayments()
        {
            return await _context.SalaryPayments.ToListAsync();
        }

        //GET: api/Game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryPayment>> GetSalaryPayment(long id)
        {
            var salaryPayment = await _context.SalaryPayments.FindAsync(id);
            if (salaryPayment == null)
            {
                return NotFound();
            }

            return salaryPayment;
        }

        // // PUT: api/Game/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutGame(long id, Game game) {
        //     if (id != game.GameId) {
        //         return BadRequest();
        //     }

        //     _context.Entry(game).State = EntityState.Modified;

        //     foreach (EquipmentUsage item in game.EquipmentUsages) {
        //         if (item.EquipmentUsageId == 0)
        //             _context.EquipmentUsages.Add(item);
        //         else
        //             _context.Entry(item).State = EntityState.Modified;
        //     }

        //     foreach (var i in game.DeletedOrderItemIds.Split(',').Where(x => x !="")) {
        //         OrderDetail y = _context.OrderDetails.Find(Convert.ToInt64(i));
        //         _context.OrderDetails.Remove(y);
        //     }

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!OrderMasterExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalaryPayment>> PostSalaryPayment(SalaryPayment salaryPayment)
        {
            _context.SalaryPayments.Add(salaryPayment);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetSalaryPayment", new { id = salaryPayment.PaymentId}, salaryPayment);
            return CreatedAtAction("GetSalaryPayment", new {id = salaryPayment.PaymentId}, salaryPayment);
        }

        // DELETE: api/Game/5
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