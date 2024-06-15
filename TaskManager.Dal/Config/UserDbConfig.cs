using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Dto;
using TaskManager.Dto.Helper;

namespace TaskManager.Dal.Config
{
    public class UserDbConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(u => u.FirstName).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(u => u.LastName).HasColumnName("surname").HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasColumnName("email").IsRequired();
            builder.Property(u => u.Phone).HasColumnName("phone").HasMaxLength(20);
            builder.Property(u => u.Role).HasColumnName("role").IsRequired();
            builder.Property(u => u.RegistrDate).HasColumnName("registr_date").IsRequired();
            builder.Property(u => u.LastLoginDate).HasColumnName("lastlogin_date").IsRequired();
            builder.Property(u => u.Avatar).HasColumnName("avatar");

            //builder.HasMany(u => u.Projects).WithMany(p => p.Users)
            //    .UsingEntity(l => l.ToTable("users_projects"));

            builder.HasData(new User()
            {
                Id = 1,
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.com",
                Password = ("qwerty").HashSha256(),
                Role = UserRole.SystemOwner
            });
        }
    }
}
