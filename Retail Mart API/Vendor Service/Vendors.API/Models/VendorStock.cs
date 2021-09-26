using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendors.API.Models
{
    /*
     * Model Used For Storing Stock
     */
    public class VendorStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public Vendor Vendor { get; set; }
    }
}
