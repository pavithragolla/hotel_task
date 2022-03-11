namespace task.DTOs;

public record ScheduleDTO
{
    public int ScheduleId { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    public DateTimeOffset Login { get; set; }
    public DateTimeOffset Logout { get; set; }
    public DateTimeOffset Date { get; set; }
}
public record ScheduleCreateDTO
{
    public int ScheduleId { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    public DateTimeOffset Login { get; set; }
    public DateTimeOffset Logout { get; set; }
    public DateTimeOffset Date { get; set; }
}