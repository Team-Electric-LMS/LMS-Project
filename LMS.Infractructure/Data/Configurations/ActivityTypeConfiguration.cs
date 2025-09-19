using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infractructure.Data.Configurations;
public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
{
    public void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        builder.ToTable("ActivityTypes");
        builder.HasKey(at => at.Id);

        builder.HasMany(at => at.Activities)
               .WithOne(a => a.ActivityType)
               .HasForeignKey(a => a.ActivityTypeId);
    }
}
