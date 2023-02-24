namespace Exl.ResumeBuilder.Web.Data
{
    public class Education
    {
        public Guid Id  { get; set; }
        public Guid ResumeId  { get; set; }
        public Resume Resume { get; set; }

        public DateTime StartDate  { get; set; }
        public DateTime EndDate  { get; set; }
        public bool IsStillStudiengHere  { get; set; }
        public string FacilityName  { get; set; }
        public string EducationTitle  { get; set; }
        public string Level  { get; set; }
        public string Description  { get; set; }
    }
}