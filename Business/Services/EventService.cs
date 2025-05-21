using Business.Dtos;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class EventService(DataContext Context) : IEventService
{
    private readonly DataContext _context = Context;
    public async Task<EventDto> CreateEventAsync(EventDto eventDto)
    {
        try
        {
            var eventEntity = new EventEntity
            {
                Image = eventDto.Image,
                Title = eventDto.Title,
                EventDate = eventDto.EventDate,
                Location = eventDto.Location,
            };

            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();

            return MapToEventDto(eventEntity);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create event: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        try
        {
            var events = await _context.Events.ToListAsync();
            return events.Select(MapToEventDto);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve events: {ex.Message}", ex);
        }
    }

    public async Task<EventDto?> GetEventByIdAsync(string id)
    {
        try
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null) return null;

            return MapToEventDto(eventEntity);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve event with ID {id}: {ex.Message}", ex);
        }
    }

    public async Task UpdateEventAsync(EventDto eventDto)
    {
        try
        {
            var eventEntity = await _context.Events.FindAsync(eventDto.Id) ?? throw new Exception($"Event with ID {eventDto.Id} not found");
            eventEntity.Image = eventDto.Image;
            eventEntity.Title = eventDto.Title;
            eventEntity.EventDate = eventDto.EventDate;
            eventEntity.Location = eventDto.Location;

            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update event: {ex.Message}", ex);
        }
    }

    public async Task DeleteEventAsync(string id)
    {
        try
        {
            var eventEntity = await _context.Events.FindAsync(id) ?? throw new Exception($"Event with ID {id} not found");
            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete event: {ex.Message}", ex);
        }
    }

    private static EventDto MapToEventDto(EventEntity eventEntity)
    {
        return new EventDto
        {
            Id = eventEntity.Id,
            Image = eventEntity.Image,
            Title = eventEntity.Title,
            EventDate = eventEntity.EventDate,
            Location = eventEntity.Location,
        };
    }
}
