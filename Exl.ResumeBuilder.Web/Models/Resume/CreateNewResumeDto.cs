using System.ComponentModel.DataAnnotations;

namespace Exl.ResumeBuilder.Web.Models.Resume
{
    public class CreateNewResumeDto
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(maximumLength: 32,MinimumLength = 3, ErrorMessage = "Full Name must be at least 3 characters long and less than 32 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(maximumLength: 32, ErrorMessage = "Job Title length can not exceed 32 characters")]
        public string JobTitle { get; set; }
        
        [StringLength(maximumLength: 42, ErrorMessage = "Location length can not exceed 42 characters")]
        public string Location { get; set; }

        [StringLength(maximumLength: 32, ErrorMessage = "Phone length can not exceed 26 characters")]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(maximumLength: 42, ErrorMessage = "Email length can not exceed 42 characters")]
        public string Email { get; set; }
        public string[] Tags { get; set; }

        [ValidateComplexType]
        public List<ExperienceDto> Experiences { get; set; }

        public CreateNewResumeDto() {
            Experiences = new List<ExperienceDto>();
        }
    }
}
