using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProceedToBuy.API.Models
{
    public class CustomerWishlistModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
       public DateTime DateAddedToWishlist { get; set; }


    }
}
