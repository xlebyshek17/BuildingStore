using BuildingStore.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.ViewModels
{
    public class ProductViewModel : EditImageViewModel
    {

        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Name not specified")]
        //[Remote(action: "CheckName", controller: "Product", ErrorMessage = "This product is already exist")]
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Incorrect name")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 45 characters")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Incorrect price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is not specified")]
        public string Description { get; set; }

        public Category Category { get; set; }
    }
}
