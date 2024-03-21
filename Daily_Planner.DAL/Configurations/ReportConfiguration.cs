using Daily_Planner.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Daily_Planner.DAL.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
        
        //test data for database
        builder.HasData(new List<Report>()
        {
            new Report()
            {
                Id = 1,
                Name = "Report #1",
                Description = "Test report description",
                UserId = 1,
                CreatedAt = DateTime.UtcNow
            }
        });
    }
}