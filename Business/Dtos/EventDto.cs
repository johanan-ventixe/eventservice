namespace Business.Dtos;

public class EventDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Image { get; set; }
    public string? Title { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string? Location { get; set; }
}