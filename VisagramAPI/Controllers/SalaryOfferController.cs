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
    public class SalaryOfferController : ControllerBase
    {
        private readonly VisagramDbContext _context;
        public SalaryOfferController(VisagramDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryOffer>>> GetSalaryOffers()
        {
            return await _context.SalaryOffers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryOffer>> GetSalaryOffer(long id)
        {
            var offer = await _context.SalaryOffers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            return offer;
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
        public async Task<ActionResult<SalaryOffer>> PostSalaryDetail(SalaryOffer offer)
        {
            _context.SalaryOffers.Add(offer);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetSalaryPayment", new { id = salaryPayment.PaymentId}, salaryPayment);
            return CreatedAtAction("GetSalaryOffer", new { id = offer.SalaryOfferId }, offer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryOffers(long id)
        {
            var offer = await _context.SalaryOffers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            _context.SalaryOffers.Remove(offer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaryDetailOffers(long id)
        {
            return _context.SalaryOffers.Any(e => e.SalaryOfferId == id);
        }
    }
}