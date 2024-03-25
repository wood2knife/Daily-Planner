using Daily_Planner.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Daily_Planner.DAL.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RefreshToken).IsRequired();
        builder.Property(x => x.RefreshTokenExpirytime).IsRequired();
        
        //test data for database
        builder.HasData(new List<UserToken>()
        {
            new UserToken()
            {
                Id = 1,
                RefreshToken = "TestToken",
                RefreshTokenExpirytime = DateTime.UtcNow.AddDays(7),
                UserId = 1
            }
        });
    }
}