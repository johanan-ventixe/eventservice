using Business.Dtos;

namespace Business.Services;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto?> GetEventByIdAsync(string id);
    Task<EventDto> CreateEventAsync(EventDto eventDto);
    Task UpdateEventAsync(EventDto eventDto);
    Task DeleteEventAsync(string id);
}
