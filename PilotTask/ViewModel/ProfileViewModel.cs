using System;
using System.ComponentModel.DataAnnotations;

namespace PilotTask.ViewModel
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string EmailId { get; set; }
    }
}