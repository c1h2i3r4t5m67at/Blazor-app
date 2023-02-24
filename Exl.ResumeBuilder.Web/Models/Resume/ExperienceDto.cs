using System.ComponentModel.DataAnnotations;

namespace Exl.ResumeBuilder.Web.Models.Resume
{
    public class ExperienceDto
    {
        [Required(ErrorMessage = "This field is required")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsStillWorkingHere { get; set; }
        public bool IsInEditMode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Role { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string OrganisationName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public List<ListItemDto<string>> Responsibilities { get; set; }

        public List<ListItemDto<string>> Skills { get; set; }

        public ExperienceDto() {
            StartDate = new DateTime(DateTime.Now.Year,1,1);
            EndDate = DateTime.Now;
            Responsibilities = new List<ListItemDto<string>>();
            Skills = new List<ListItemDto<string>>();
        }

    }
}
