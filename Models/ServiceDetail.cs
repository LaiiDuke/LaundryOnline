using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaundryV3.Models
{
    public class ServiceDetail
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public String Name { get; set; }
        public double Price { get; set; }
        [MaxLength(500)]
        public String Description { get; set; }
        public int ServiceLaundryId { get; set; }
        public virtual ServiceLaundry ServiceLaundry { get; set; }
    }
}