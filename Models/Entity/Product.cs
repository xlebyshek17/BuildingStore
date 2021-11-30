using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BuildingStore.Models.Entity
{
    public class Product
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Category is not specified")]
        public int CategoryID { get; set; }

        [Remote(action: "CheckName", controller: "Product", ErrorMessage = "This product is already exist")]
        [Required(ErrorMessage = "Name not specified")]
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Incorrect name")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 45 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Image is not specified")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Incorrect price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is not specified")]
        public string Description { get; set; }

        public Category Category { get; set; }
    }
}
