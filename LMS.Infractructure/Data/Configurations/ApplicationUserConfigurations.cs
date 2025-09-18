using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infractructure.Data.Configurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");
        //Add more configurations here

        builder.Property(c => c.UserName)
               .IsRequired()
               .HasMaxLength(50
             );
        builder.Property(c => c.FirstName)
               .IsRequired()
               .HasMaxLength(50
             );
        builder.Property(c => c.LastName)
               .IsRequired()
               .HasMaxLength(50
             );

        builder.HasOne(u => u.Course)
       .WithMany(c => c.Students)
       .HasForeignKey(u => u.CourseId)
       .OnDelete(DeleteBehavior.Restrict);
    }
}
