using Exl.ResumeBuilder.Web.Data;
using Exl.ResumeBuilder.Web.Models.Resume;
using Microsoft.EntityFrameworkCore;

namespace Exl.ResumeBuilder.Web.Services
{
    public class ResumeService : IResumeService
    {
        private ApplicationDbContext dbContext;
        public List<Resume> Resumes { get; set; } = new List<Resume>();
        public ResumeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateNewResume(CreateNewResumeDto newResumeDto)
        {
            var resume = new Resume()
            {
              
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                FullName = newResumeDto.FullName,
                Phone = newResumeDto.Phone,
                Email = newResumeDto.Email,
                JobTitle = newResumeDto.JobTitle,
                Location = newResumeDto.Location,
                UrlId = newResumeDto.FullName.Replace(" ", "-")
            };

            await dbContext.Resumes.AddAsync(resume);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task LoadResumes()
        {
            Resumes = await dbContext.Resumes.ToListAsync();
          
        }
		public async Task<Resume> GetResume(int id)
		{
            var resume = await dbContext.Resumes.FindAsync(id);
			return resume; 
        }
    }
}