using System;
using System.ComponentModel.DataAnnotations;

namespace PilotTask.ViewModel
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public int Status { get; set; }
        public int ProfileId { get; set; }
    }
}