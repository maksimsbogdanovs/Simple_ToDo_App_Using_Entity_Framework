using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDoList
    {
        [Key]
        public int ToDoId { get; set; }

        [MaxLength(5000)]
        [DisplayName("Things To-Do")]
        [Required(ErrorMessage ="This field is required")]
        public string ToDoListItem { get; set; }

        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Is Done")]
        public bool IsDone { get; set; }
       
    }

}
