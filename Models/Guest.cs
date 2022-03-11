using task.DTOs;

namespace task.Models;

public record Guest
{
    public int GuestId { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public DateTimeOffset Date { get; set; }

   
    public GuestDTO asDto => new GuestDTO
    {
        GuestId =GuestId,
        Name = Name,
        Details = Details,
        Date = Date,
    };
}