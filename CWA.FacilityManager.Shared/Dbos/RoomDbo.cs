namespace CWA.FacilityManager.Infrastructure.Dbos;

public class RoomDbo
{
    public Guid Id { get; set; }

    public string Location { get; set; }

    public int CoutOfSeats { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
    
    public bool IsAvailable { get; set; }
}