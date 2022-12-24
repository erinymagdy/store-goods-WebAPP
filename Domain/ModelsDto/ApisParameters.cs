using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class ApisParameters
    {
        [Required]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Invalid GoodID Number")]
        public string GoodID { get; set; }
        [Required]
        public DateTime StartPeriod { get; set; }
        [Required]
        public DateTime EndPeriod { get; set; }
    }
}
