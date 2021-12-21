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
    public class StaffController : ControllerBase
    {
        private readonly VisagramDbContext _context;
        public StaffController(VisagramDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            return await _context.Staffs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(long id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            return staff;
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
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetSalaryPayment", new { id = salaryPayment.PaymentId}, salaryPayment);
            return CreatedAtAction("GetStaff", new { id = staff.StaffId }, staff);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(long id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffExists(long id)
        {
            return _context.Staffs.Any(e => e.StaffId == id);
        }
    }
}