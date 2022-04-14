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
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set;}
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set;}
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set;}
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set;}
        public DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }
        public DbSet<CompanyJobEducationPoco> companyJobEducations { get; set; }
        public DbSet<CompanyJobSkillPoco> companyJobSkills { get; set;}
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobDescriptionPoco> companyJobDescriptions { get; set; }
        public DbSet<CompanyLocationPoco> companyLocation { get; set; }
        public DbSet<SecurityLoginsLogPoco> securityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> securityLoginsRole { get; set;}
        public DbSet <SystemLanguageCodePoco> systemLanguageCodes { get; set; }
        public DbSet<SystemCountryCodePoco> systemCountryCode { get; set; }
        public DbSet<SecurityRolePoco> securityRoles { get; set; }
        public DbSet<SecurityLoginPoco> securityLogin { get; set; }
        public DbSet<CompanyProfilePoco> companyProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-DNVP3G7\\WINTER2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>(
                entity => entity.HasOne(c => c.ApplicantProfile)
                .WithMany(t => t.ApplicantEducation)
                .HasForeignKey(K => K.Applicant)
                );
            modelBuilder.Entity<ApplicantEducationPoco>().Ignore(c => c.TimeStamp);
        }
    }
}
