namespace Exl.ResumeBuilder.Web.Data
{
    public class Experience
    {
        public Guid Id { get; set; }
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; }

        public DateTime StartDate  { get; set; }
        public DateTime EndDate  { get; set; }
        public bool IsStillWorkingHere  { get; set; }
        public string Role  { get; set; }
        public string OrganisationName  { get; set; }
        public string Description  { get; set; }
        public string Responsibilities { get; set; }
        public IEnumerable<Skill> InvolvedSkills { get; set; }

        // Todo: allow images?
    }
}