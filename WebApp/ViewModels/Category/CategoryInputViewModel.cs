using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Category
{
    public class CategoryInputViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(64, ErrorMessage = "Max name length 64 characters")]
        public string Name { get; set; }
    }
}
