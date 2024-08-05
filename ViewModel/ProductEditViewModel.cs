using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using INFT3050.Models;

namespace INFT3050.ViewModel
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter price number.")]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter how many stock.")]
        [Range(1, 10000, ErrorMessage = "Stock must be between 1 and 10000.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; }

        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }

        public string ImagePath;

        [Required(ErrorMessage = "Please select product status.")]
        public ProductStatus Status { get; set; }
    }
}
