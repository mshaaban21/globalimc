using GlobalimcTaskBE.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalimcTaskBE.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VendorId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public string Image { get; set; }

        [Required]
        public DietaryFlags DietaryFlag { get; set; }

        [Required]
        public int ViewsCount { get; set; }
    }
}
