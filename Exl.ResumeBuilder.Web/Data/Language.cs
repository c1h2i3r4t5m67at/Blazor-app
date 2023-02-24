namespace Exl.ResumeBuilder.Web.Data
{
    public class Language
    {
        public Guid Id { get; set; }
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; }
        public string Name { get; set; }
        public string SpokenLevel { get; set; }
        public string WrittenLevel { get; set; }
    }
}