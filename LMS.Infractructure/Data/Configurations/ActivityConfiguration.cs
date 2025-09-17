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

        builder.HasOne(a => a.Module)
               .WithMany(m => m.Activities)
               .HasForeignKey(a => a.ModuleId);

        builder.HasOne(a => a.ActivityType)
               .WithMany(at => at.Activities)
               .HasForeignKey(a => a.ActivityTypeId);
    }
}
