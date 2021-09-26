using System.ComponentModel.DataAnnotations;

namespace Retail_Product_Management_System.Models
{
    /*
     * Model Used For ProductDetail
     */
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image_Name { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int No_Of_Units { get; set; }
    }
}
