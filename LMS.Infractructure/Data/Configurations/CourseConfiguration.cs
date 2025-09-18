using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Infractructure.Data.Configurations;
public class CourseConfiguration: IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Modules)
               .WithOne(m => m.Course)
               .HasForeignKey(m => m.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Documents)
               .WithOne(d => d.Course)
               .HasForeignKey(d => d.CourseId);

        builder.HasMany(c => c.Students)
               .WithOne(s => s.Course)
               .HasForeignKey(s => s.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Teachers)
               .WithMany(t => t.CoursesTaught) 
               .UsingEntity(j => j.ToTable("CourseTeachers")); 



    }
}
