using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DAL.Models;

namespace TaskManager.Dal.Config
{
    public class DeskDbConfig : IEntityTypeConfiguration<Desk>
    {
        public void Configure(EntityTypeBuilder<Desk> builder)
        {
            builder.Property(desk => desk.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(desk => desk.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(desk => desk.Description).HasColumnName("description").HasMaxLength(200);
            builder.Property(desk => desk.DeskColumns).HasColumnName("desk_columns").IsRequired();
            builder.Ignore(desk => desk.CreatedDate);
            builder.Ignore(desk => desk.EndDate);
            builder.Ignore(desk => desk.Image);

            builder.HasOne(desk => desk.Project).WithMany(proj => proj.Desks)
                .HasPrincipalKey(proj => proj.Id).HasForeignKey(desk => desk.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
