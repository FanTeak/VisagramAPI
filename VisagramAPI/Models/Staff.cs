using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VisagramAPI.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string StaffName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string StaffSurname { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string Phone { get; set; }
    }
}
