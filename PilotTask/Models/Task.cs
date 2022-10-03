
using System;

namespace PilotTask.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int Status { get; set; }
        public int ProfileId { get; set; }
    }
}