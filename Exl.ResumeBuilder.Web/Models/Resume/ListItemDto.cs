namespace Exl.ResumeBuilder.Web.Models.Resume
{
    public class ListItemDto<T>
    {
        public T Value { get; set; }
        public int Order { get; set; }
        public Guid Id { get; set; }

        public ListItemDto() {
            Id = Guid.NewGuid();
        }
    }
}