using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaundryV3.Models
{    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public String Name { get; set; }
        [MinLength(10), MaxLength(12), Phone]
        public String Phone { get; set; }
        [MaxLength(100), EmailAddress]
        public String Email { get; set; }
        public String Address { get; set; }
    }
}
