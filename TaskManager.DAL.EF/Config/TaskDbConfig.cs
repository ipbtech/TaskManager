using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DAL.Models;

namespace TaskManager.Dal.Config
{
    public class TaskDbConfig : IEntityTypeConfiguration<WorkTask>
    {
        public void Configure(EntityTypeBuilder<WorkTask> builder)
        {
            builder.Property(task => task.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(task => task.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(task => task.Description).HasColumnName("description").HasMaxLength(200);
            builder.Property(task => task.CreatedDate).HasColumnName("create_date").IsRequired();
            builder.Property(task => task.ColumnOfDesk).HasColumnName("column").IsRequired();
            builder.Property(task => task.AttachmentsData).HasColumnName("attachments");
            builder.Ignore(task => task.Image);

            builder.Property(task => task.StartDate).HasColumnName("start_date");
            builder.Property(task => task.EndDate).HasColumnName("end_date");


            builder.HasOne(task => task.Creator).WithMany(user => user.CreatingTasks)
                .HasPrincipalKey(user => user.Id).HasForeignKey(task => task.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(task => task.Contractor).WithMany(user => user.AssigningTasks)
                .HasPrincipalKey(user => user.Id).HasForeignKey(task => task.ContractorId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(task => task.Desk).WithMany(desk => desk.Tasks)
                .HasPrincipalKey(desk => desk.Id).HasForeignKey(task => task.DeskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
