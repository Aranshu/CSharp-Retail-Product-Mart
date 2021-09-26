using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Product_Management_System.Models
{
    /*
     * Model Used For Storing Customer WishList
     */
    public class CustomerWishlistModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime DateAddedToWishlist { get; set; }


    }
}
