using Daily_Planner.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Daily_Planner.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Password).IsRequired();
        builder.HasMany<Report>(x => x.Reports)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id);
        
        //test data for database
        builder.HasData(new List<User>()
        {
            new User()
            {
                Id = 1,
                Login = "testUser",
                Password = "testUserPass",
                CreatedAt = DateTime.UtcNow
            }
        });
    }
}