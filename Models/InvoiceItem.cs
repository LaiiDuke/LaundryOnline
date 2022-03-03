using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaundryV3.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public String Code { get; set; }
        public int ServiceDetailId { get; set; }
        public virtual ServiceDetail ServiceDetail { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}