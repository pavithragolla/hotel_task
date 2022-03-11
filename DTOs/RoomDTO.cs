using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task.DTOs;

public record RoomDTO
{
    [JsonPropertyName("room_id")]
    public int RoomId { get; set; }
    [JsonPropertyName("staff_id")]
    public int StaffId { get; set; }
    
    [JsonPropertyName("staff")]
    public List<StaffDTO> Staff { get; internal set; }
   
}
public record RoomCreateDTO
{
    [JsonPropertyName("room_id")]
    [Required]

    public int RoomId { get; set; }

    [JsonPropertyName("Staff_id")]
    [Required]
    public int StaffId { get; set; }

}