using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Task
{
    public class TaskInputViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(128, ErrorMessage = "Max title length 128 characters")]
        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }
    }
}
