using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaundryV3.Models
{
    public class ServiceLaundry
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public String Description { get; set; }
    }
}