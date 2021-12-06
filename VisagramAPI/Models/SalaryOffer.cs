using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VisagramAPI.Models
{
    public class SalaryOffer
    {
        [Key]
        public int SalaryOfferId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string OfferName { get; set; }

        public decimal OfferPrice { get; set; }
    }
}
