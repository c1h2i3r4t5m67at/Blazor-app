using Exl.ResumeBuilder.Web.Data;

namespace Exl.ResumeBuilder.Web.Services
{
    public interface IResumeService
    {
        List<Resume> Resumes { get; set; }
        Task LoadResumes();
        Task<Resume> GetResume(int id);
	}
}
