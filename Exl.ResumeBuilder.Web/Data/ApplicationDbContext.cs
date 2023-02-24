using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exl.ResumeBuilder.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Resume> Resumes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}