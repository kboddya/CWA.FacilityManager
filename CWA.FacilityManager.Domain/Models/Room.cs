namespace CWA.FacilityManager.Domain.Models;

public class Room
{
    public Guid Id { get; set; }

    public string Location { get; set; }

    public uint CoutOfSeats { get; set; }
    
    public string? Description { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public bool? IsAvailable { get; set; }
}