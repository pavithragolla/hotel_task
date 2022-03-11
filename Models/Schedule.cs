using task.DTOs;

namespace task.Models;

public record Schedule
{
    public int ScheduleId { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    public DateTimeOffset Login { get; set; }
    public DateTimeOffset Logout { get; set; }

    public DateTimeOffset Date { get; set; }


    public ScheduleDTO asDto => new ScheduleDTO
    {
        ScheduleId = ScheduleId,
        RoomId = RoomId,
        GuestId = GuestId,
        Login = Login,
        Logout = Logout,
        Date = Date
    };
}