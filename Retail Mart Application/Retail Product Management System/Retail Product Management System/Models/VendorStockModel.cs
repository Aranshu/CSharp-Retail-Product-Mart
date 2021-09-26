using System;
using System.ComponentModel.DataAnnotations;

namespace Retail_Product_Management_System.Models
{
    /*
     * Model Used For ProductStock Avalible with Vendor
     */
    public class VendorStockModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int HandInStocks { get; set; }

        [Required]
        public DateTime ReplinshmentDate { get; set; }

        [Required]
        public VendorModel Vendor { get; set; }
    }
}
