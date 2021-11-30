using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Entity
{
    public class Category
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name not specified")]
        [Remote(action: "CheckCategory", controller: "Category", ErrorMessage = "This category is already exist")]
        [RegularExpression(@"^[\w]*$", ErrorMessage = "Incorrect name")]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
