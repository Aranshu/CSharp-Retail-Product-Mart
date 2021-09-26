using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProceedToBuy.API.Models
{
    public class CartModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int CustomerId{get; set;}
        public int VendorId { get; set; }

        public int ProductId { get; set; }
        public int Zipcode { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
