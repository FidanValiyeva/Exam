using Exam1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam1.DAL
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public AppDbContext(DbContextOptions opts):base(opts)
        {
                
        }
    }
}
