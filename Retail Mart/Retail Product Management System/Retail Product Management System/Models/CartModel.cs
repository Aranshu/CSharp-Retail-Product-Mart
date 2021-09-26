using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Product_Management_System.Models
{
    public class CartModel
    {
        /*
         * Model Used For Storing Cart Details
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [Required]
        public int CustomerId{get; set;}

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Zipcode { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}
