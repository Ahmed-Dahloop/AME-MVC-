using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AME.Models
{
    public class AMEDbContext:DbContext
    {

        public AMEDbContext() : base()
        {

        }
        public AMEDbContext(DbContextOptions<AMEDbContext> options) : base(options)
        {
        }
     
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
  
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
          optionsBuilder.UseSqlServer("Server=DAHLOOP\\SQLSERVER;Database=AMESMDB;Trusted_Connection=True;TrustServerCertificate=True");
          base.OnConfiguring(optionsBuilder);   
        }

    }
}
