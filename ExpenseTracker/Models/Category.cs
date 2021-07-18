using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string Housing { get; set; }
        public string Transportation { get; set; }
        public string Food { get; set; }
        public string Utilities { get; set; }
        public string Insurance { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public ICollection<SubCategory> SubCategory { get; set; }
    }
}
