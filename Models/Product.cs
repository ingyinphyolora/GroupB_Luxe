using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace INFT3050.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter price number.")] 
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter how many stock.")]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; } = string.Empty;

        [ValidateNever]
        public Category Category { get; set; } = null!;

        [DisplayName("Upload Image.")]
        [Required(ErrorMessage = "Please input a picture.")]
        public string ImagePath { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Please select product status. ")]
        public ProductStatus Status { get; set; }

    }
}
