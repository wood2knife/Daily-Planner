using Daily_Planner.Domain.Interfaces;

namespace Daily_Planner.Domain.Entity;

public class UserToken : IEntityId<long>
{
    public long Id { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpirytime { get; set; }
    public User User { get; set; }
    public long UserId { get; set; }
}