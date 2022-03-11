
using task.DTOs;

namespace task.Models;

public record Room
{
    public int RoomId { get; set; }
    public int StaffId { get; set; }

    public RoomDTO asDto => new RoomDTO
    {
        RoomId = RoomId,
        StaffId = StaffId,
    };

}

