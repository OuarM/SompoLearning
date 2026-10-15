using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = "Not Started";

        public int? UserId { get; set; }

        
    }
}