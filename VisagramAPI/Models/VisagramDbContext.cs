using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VisagramAPI.Models
{
    public class VisagramDbContext : DbContext
    {
        public VisagramDbContext(DbContextOptions<VisagramDbContext> options) : base(options)
        {
            
        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<SalaryOffer> SalaryOffers { get; set; }
        public DbSet<SalaryPayment> SalaryPayments { get; set; }
        public DbSet<SalaryDetails> SalaryDetails { get; set; }
    }
}
