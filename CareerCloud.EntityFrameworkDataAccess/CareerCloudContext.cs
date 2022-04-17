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
        public DbSet<CompanyLocationPoco> companyLocations { get; set; }
        public DbSet<SecurityLoginsLogPoco> securityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> securityLoginsRoles { get; set;}
        public DbSet <SystemLanguageCodePoco> systemLanguageCodes { get; set; }
        public DbSet<SystemCountryCodePoco> systemCountryCodes { get; set; }
        public DbSet<SecurityRolePoco> securityRoles { get; set; }
        public DbSet<SecurityLoginPoco> securityLogins { get; set; }
        public DbSet<CompanyProfilePoco> companyProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-DNVP3G7\\WINTER2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            //Applicant eduaction
            modelBuilder.Entity<ApplicantEducationPoco>(
                entity => entity.HasOne(c => c.ApplicantProfile)
                .WithMany(t => t.ApplicantEducations)
                .HasForeignKey(k => k.Applicant)
                );
            //ApplicantJobApplication
            modelBuilder.Entity<ApplicantJobApplicationPoco>(
                entity => entity.HasOne(c => c.ApplicantProfile)
                .WithMany(t => t.ApplicantJobApplications)
                .HasForeignKey(k => k.Applicant)
                );
            modelBuilder.Entity<ApplicantJobApplicationPoco>(
                entity => entity.HasOne(c => c.CompanyJob)
                .WithMany(t => t.ApplicantJobApplications)
                .HasForeignKey(k => k.Job)
                );
            //Applicant profile
            modelBuilder.Entity<ApplicantProfilePoco>(
               entity => entity.HasOne(c => c.SecurityLogin)
               .WithMany(t => t.ApplicantProfiles)
               .HasForeignKey(k => k.Login)
               );
            modelBuilder.Entity<ApplicantProfilePoco>(
               entity => entity.HasOne(c => c.SystemCountryCode)
               .WithMany(t => t.ApplicantProfile)
               .HasForeignKey(k => k.Country)
               );

            //ApplicantResumePoco
            modelBuilder.Entity<ApplicantResumePoco>(
               entity => entity.HasOne(c => c.ApplicantProfile)
               .WithMany(t => t.ApplicantResumes)
               .HasForeignKey(k => k.Applicant)
               );

            //ApplicantSkillPoco
            modelBuilder.Entity<ApplicantSkillPoco>(
              entity => entity.HasOne(c => c.ApplicantProfile)
              .WithMany(t => t.ApplicantSkills)
              .HasForeignKey(k => k.Applicant)
              );

            //ApplicantWorkHistory
            modelBuilder.Entity<ApplicantWorkHistoryPoco>(
              entity => entity.HasOne(c => c.ApplicantProfile)
              .WithMany(t => t.ApplicantWorkHistorys)
              .HasForeignKey(k => k.Applicant)
              );
            modelBuilder.Entity<ApplicantWorkHistoryPoco>(
              entity => entity.HasOne(c => c.SystemCountryCode)
              .WithMany(t => t.ApplicantWorkHistory)
              .HasForeignKey(k => k.CountryCode)
              );

            //CompanyDescriptionPoco
            modelBuilder.Entity<CompanyDescriptionPoco>(
              entity => entity.HasOne(c => c.CompanyProfile)
              .WithMany(t => t.CompanyDescriptions)
              .HasForeignKey(k => k.Company)
              );

            modelBuilder.Entity<CompanyDescriptionPoco>(
             entity => entity.HasOne(c => c.SystemLanguageCode)
             .WithMany(t => t.CompanyDescriptions)
             .HasForeignKey(k => k.LanguageId)
             );

            //CompanyJobDescriptionPoco
            modelBuilder.Entity<CompanyJobDescriptionPoco>(
            entity => entity.HasOne(c => c.CompanyJob)
            .WithMany(t => t.CompanyJobDescriptions)
            .HasForeignKey(k => k.Job)
            );

            //CompanyJobEducationPoco

           modelBuilder.Entity<CompanyJobEducationPoco>(
           entity => entity.HasOne(c => c.CompanyJob)
           .WithMany(t => t.CompanyJobEducations)
           .HasForeignKey(k => k.Job)
           );

            //CompanyJobPoco

            modelBuilder.Entity<CompanyJobPoco>(
           entity => entity.HasOne(c => c.CompanyProfile)
           .WithMany(t => t.CompanyJobs)
           .HasForeignKey(k => k.Company)
           );

            //CompanyJobSkill
            modelBuilder.Entity<CompanyJobSkillPoco>(
          entity => entity.HasOne(c => c.CompanyJob)
          .WithMany(t => t.CompanyJobSkills)
          .HasForeignKey(k => k.Job)
          );
            //CompanyLocationPoco

         modelBuilder.Entity<CompanyLocationPoco>(
        entity => entity.HasOne(c => c.CompanyProfile)
        .WithMany(t => t.CompanyLocations)
        .HasForeignKey(k => k.Company)
        );
        modelBuilder.Entity<CompanyLocationPoco>(
       entity => entity.HasOne(c => c.SystemCountryCode)
       .WithMany(t => t.CompanyLocation)
       .HasForeignKey(k => k.CountryCode)
       );
            // SecurityLoginsLogPoco
            modelBuilder.Entity<SecurityLoginsLogPoco>(
        entity => entity.HasOne(c => c.SecurityLogin)
        .WithMany(t => t.SecurityLoginsLogs)
        .HasForeignKey(k => k.Login)
        );
            //SecurityLoginsRolePoco
        modelBuilder.Entity<SecurityLoginsRolePoco>(
       entity => entity.HasOne(c => c.SecurityLogin)
       .WithMany(t => t.SecurityLoginsRoles)
       .HasForeignKey(k => k.Login)
       );
        modelBuilder.Entity<SecurityLoginsRolePoco>(
       entity => entity.HasOne(c => c.SecurityRole)
       .WithMany(t => t.SecurityLoginsRoles)
       .HasForeignKey(k => k.Role)
       );
            
           modelBuilder.Entity<ApplicantEducationPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<ApplicantJobApplicationPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<ApplicantProfilePoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<ApplicantSkillPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<CompanyDescriptionPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<CompanyJobDescriptionPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<CompanyJobEducationPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<CompanyJobPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<CompanyJobSkillPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<CompanyLocationPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<CompanyProfilePoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<SecurityLoginPoco>().Ignore(c => c.TimeStamp);
            modelBuilder.Entity<SecurityLoginsRolePoco>().Ignore(c => c.TimeStamp);
        }
    }
}
