namespace Exl.ResumeBuilder.Web.Data
{
    public class Skill
    {
        public Guid Id { get; set; }
        public Guid ExperianceId { get; set; }
        public Experience Experiance { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsTopSkill { get; set; }
        public int Order { get; set; }
    }
}