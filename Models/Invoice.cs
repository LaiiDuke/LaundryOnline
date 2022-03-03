using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaundryV3.Models
{
    public enum DeliveryStatus
    {
        Ready, Pending, Delivered
    }
    public enum PayMethod
    {
        COD_PICK, COD_RETURN, BANKING
    }
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public double CustomerPaid { get; set; }
        public double ShippingFee { get; set; }
        [MaxLength(500)]
        public String Description { get; set; }
        public String Code { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime PickAt { get; set; }
        public DateTime ReturnAt { get; set; }
        public String PickAddress { get; set; }
        public String ReturnAddress { get; set; }
        public DeliveryStatus? PickStatus { get; set; }
        public DeliveryStatus? ReturnStatus { get; set; }
        public PayMethod? PayMethod { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<InvoiceItem> ListItems { get; set; }
    }
}