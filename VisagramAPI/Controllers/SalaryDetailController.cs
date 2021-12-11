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
    public class SalaryDetailController : ControllerBase
    {
        private readonly VisagramDbContext _context;
        public SalaryDetailController(VisagramDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryDetails>>> GetSalaryDetails()
        {
            return await _context.SalaryDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryDetails>> GetSalaryDetail(long id)
        {
            var detail = await _context.SalaryDetails.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }

            return detail;
        }

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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalaryDetails>> PostSalaryDetail(SalaryDetails detail)
        {
            _context.SalaryDetails.Add(detail);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetSalaryPayment", new { id = salaryPayment.PaymentId}, salaryPayment);
            return CreatedAtAction("GetSalaryDetail", new { id = detail.SalaryDetailsId }, detail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryDetails(long id)
        {
            var detail = await _context.SalaryDetails.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }

            _context.SalaryDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaryDetailExists(long id)
        {
            return _context.SalaryDetails.Any(e => e.SalaryDetailsId == id);
        }
    }
}