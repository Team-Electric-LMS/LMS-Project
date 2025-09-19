using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infractructure.Data.Configurations;
public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        builder.HasKey(d => d.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(50
             );

        builder.Property(c => c.Link)
               .IsRequired();

        builder.Property(c => c.Description)
               .HasMaxLength(50);

        builder.HasOne(d => d.Course)
               .WithMany(c => c.Documents)
               .HasForeignKey(d => d.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Module)
               .WithMany(m => m.Documents)
               .HasForeignKey(d => d.ModuleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Activity)
               .WithMany(a => a.Documents)
               .HasForeignKey(d => d.ActivityId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.DocumentType)
               .WithMany(dt => dt.Documents)
               .HasForeignKey(d => d.DocumentTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.UploadedBy)
               .WithMany()
               .HasForeignKey(d => d.UploadedById)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
