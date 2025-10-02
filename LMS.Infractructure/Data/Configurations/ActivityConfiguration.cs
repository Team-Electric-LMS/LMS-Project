using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infractructure.Data.Configurations;
public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
       public void Configure(EntityTypeBuilder<Activity> builder)
       {
              builder.ToTable("Activities");
              builder.HasKey(a => a.Id);

              builder.Property(c => c.Name)
                     .IsRequired()
                     .HasMaxLength(50
                   );

              builder.HasOne(a => a.Module)
                     .WithMany(m => m.Activities)
                     .HasForeignKey(a => a.ModuleId)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.HasOne(a => a.ActivityType)
                     .WithMany(at => at.Activities)
                     .HasForeignKey(a => a.ActivityTypeId);
    }
}
