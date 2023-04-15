namespace BusinessLogic.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }

        public int? CategoryId { get; set; }
    }
}
