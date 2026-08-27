using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using To_Do_Application_API.Models.Domains;

namespace To_Do_Application_API.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.IsCompleted)
                .IsRequired();

            builder.Property(t => t.Priority)
                .IsRequired()
                .HasConversion<string>() 
                .HasMaxLength(20);

            builder.Property(t => t.DueDate);

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            
            builder.HasIndex(t => new { t.UserId, t.CategoryId });
        }
    }
}
