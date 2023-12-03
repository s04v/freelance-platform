using Core.Chat.Entities;
using Core.Jobs.Applications.Entities;
using Core.Jobs.Attachment;
using Core.Jobs.Entities;
using Core.Orders.Entities;
using Core.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database
{
    public class BaseDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<JobAttachment> JobAttachment { get; set; }
        public DbSet<Message> ChatMessage { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<Order> Order { get; set; }

        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("user")
                .HasKey(o => o.Uuid);

            modelBuilder.Entity<Job>()
                .ToTable("job")
                .HasKey(o => o.Uuid);

            modelBuilder.Entity<Job>()
                .HasMany(o => o.Applications)
                .WithOne(o => o.Job)
                .HasForeignKey(o => o.JobUuid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Job>()
                .HasMany(o => o.Attachments)
                .WithOne(o => o.Job)
                .HasForeignKey(o => o.JobUuid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobApplication>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserUuid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobApplication>()
                .ToTable("job_application")
                .HasKey(o => o.Uuid);

            modelBuilder.Entity<JobAttachment>()
                .ToTable("job_attachment")
                .HasKey(o => o.Uuid);

            modelBuilder.Entity<Message>()
                .ToTable("chat_message")
                .HasKey(o => o.Uuid);

            modelBuilder.Entity<Conversation>()
                .ToTable("conversation")
                .HasKey(o => o.Uuid);

            modelBuilder.Entity<Message>()
                .HasOne(o => o.Conversation)
                .WithMany(o => o.Messages)
                .HasForeignKey(o => o.ConversationUuid);

            modelBuilder.Entity<Order>()
              .ToTable("order")
              .HasKey(o => o.Uuid);

            MapColumnName(modelBuilder);
            SetDecimalType(modelBuilder);
        }

        protected void MapColumnName(ModelBuilder modelBuilder) 
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    var columnName = Regex.Replace(property.Name, @"([a-z0-9])([A-Z]|[A-Z][a-z])", "$1_$2");
                    property.SetColumnName(columnName);
                }
            }
        }

        protected void SetDecimalType(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
             .SelectMany(t => t.GetProperties())
             .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
        }
    }
}
