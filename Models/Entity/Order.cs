using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Entity
{
    public class Order
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Name is not specified")]
        [RegularExpression(@"^[\wа-яА-Я]*$", ErrorMessage = "Incorrect name")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 45 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is not specified")]
        [RegularExpression(@"^[\wа-яА-Я]*$", ErrorMessage = "Incorrect surname")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 45 characters")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Phone is not specified")]
        [RegularExpression(@"^((\+375)[\- ]?)?(\(?\d{2}\)?[\- ]?)?[\d\- ]{7}$", ErrorMessage = "Phone number must be in format +375*********")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is not specified")]
        public string Address { get; set; }

        public User User { get; set; }
    }
}
