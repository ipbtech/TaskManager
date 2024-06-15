using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Dto;

namespace TaskManager.Dal.Config
{
    public class UserProjectLinkDbConfig : IEntityTypeConfiguration<UserProjectLink>
    {
        public void Configure(EntityTypeBuilder<UserProjectLink> builder)
        {
            builder.Property(upLink => upLink.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(upLink => upLink.UserId).HasColumnName("user_id");
            builder.Property(upLink => upLink.ProjectId).HasColumnName("project_id");

            builder.HasOne(upLink => upLink.User).WithMany(user => user.UserProjects)
                .HasPrincipalKey(user => user.Id).HasForeignKey(upLink => upLink.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(upLink => upLink.Project).WithMany(proj => proj.ProjectUsers)
                .HasPrincipalKey(proj => proj.Id).HasForeignKey(upLink => upLink.ProjectId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
