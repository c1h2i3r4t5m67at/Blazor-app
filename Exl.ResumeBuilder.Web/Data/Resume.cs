namespace Exl.ResumeBuilder.Web.Data
{
    public class Resume
    {
        public int Id { get; set; }
		public string? UserId { get; set; }
        public string UrlId { get; set; }
        public string FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public IEnumerable<Experience> Experiances { get; set; }
        public IEnumerable<Education> Educations { get; set; }
    }
}