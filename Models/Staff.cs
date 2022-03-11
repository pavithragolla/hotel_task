using task.DTOs;

namespace task.Models;

public record Staff
{
    public int StaffId { get; set; }
    public string Name { get; set; }
    public long Mobile { get; set; }
    public string Address { get; set; }
    public string Gender { get; set; }

    public StaffDTO asDto => new StaffDTO
    {
        StaffId = StaffId,
        Name = Name,
        Mobile = Mobile,
        Address = Address,
        Gender = Gender

    };
}