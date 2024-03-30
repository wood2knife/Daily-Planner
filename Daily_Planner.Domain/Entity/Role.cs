using Daily_Planner.Domain.Interfaces;

namespace Daily_Planner.Domain.Entity;

public class Role : IEntityId<long>
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public List<User> Users { get; set; }
}