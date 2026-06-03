using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Persistence.Contexts
{
	public class TeknosipDbContext : IdentityDbContext<AppUser,AppRole,Guid>
	{
		public TeknosipDbContext(DbContextOptions<TeknosipDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


			builder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany(u => u.SentMessages)
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.Restrict);


			builder.Entity<Message>()
				.HasOne(m => m.Receiver)
				.WithMany(u => u.ReceivedMessages)
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);


			builder.Entity<ProjectApplication>()
				.HasOne(p => p.Project)
				.WithMany(p => p.ProjectApplications) 
				.HasForeignKey(p => p.ProjectId)
				.OnDelete(DeleteBehavior.Cascade); 

			
			builder.Entity<ProjectApplication>()
				.HasOne(p => p.AppUser)
				.WithMany(p => p.ProjectApplications) 
				.HasForeignKey(p => p.AppUserId)
				.OnDelete(DeleteBehavior.Restrict);

		}


        public DbSet<Category> Categories { get; set; }	
		public DbSet<AcademicianProfile> AcademicianProfiles { get; set; }	
		public DbSet<CompanyProfile> CompanyProfiles { get; set; }	
		public DbSet<InstitutionProfile> InstitutionProfiles { get; set; }	
		public DbSet<StudentProfile> StudentProfiles { get; set; }	
		public DbSet<Message> Messages { get; set; }	
		public DbSet<Notification> Notifications { get; set; }	
		public DbSet<Project> Projects { get; set; }	
		public DbSet<ProjectApplication> ProjectApplications { get; set; }	
		public DbSet<ContactMessage> ContactMessages { get; set; }
		public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }


	}
}
