using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VisagramAPI.Models
{
    public class SalaryPayment
    {
        [Key]
        public long PaymentId { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string PaymentNumber { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string PaymentType { get; set; }

        public decimal Total { get; set; }

        public List<SalaryDetails> OrderDetails { get; set; }

        [NotMapped]
        public string DeletedSalaryItemIds { get; set; }
    }
}
