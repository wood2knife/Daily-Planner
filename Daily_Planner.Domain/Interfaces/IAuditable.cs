namespace Daily_Planner.Domain.Interfaces;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public DateTime UpdateAt { get; set; }
    public long UpdatedBy { get; set; }
}