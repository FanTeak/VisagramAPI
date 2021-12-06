using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisagramAPI.Models
{
    public class SalaryDetails
    {
        [Key]
        public long SalaryDetailsId { get; set; }

        public long SalaryPaymentId { get; set; }

        public int SalaryOfferId { get; set; }

        public decimal SalaryOfferPrice { get; set; }

        public float Quantity { get; set; }
    }
}
