using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace C_sharp_todo_api.Models
{
    public class TodoTask
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EnumStatus Status { get; set; }
    }
}
