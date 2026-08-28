using System.ComponentModel.DataAnnotations;

namespace StudentApi.DTOs
{
    public class StudentCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Course must be between 1 and 100 characters")]
        public string Course { get; set; } = string.Empty;
    }
}