using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-DNVP3G7\\WINTER2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>(
                entity => entity.HasOne(c => c.ApplicantProfile)
                .WithMany(t => t.ApplicantEducations)
                .HasForeginKey(K => K.Applicant)
                );
            modelBuilder.Entity<ApplicantEducationPoco>().Ignore(c => c.TimeStamp);
        }
    }
}
