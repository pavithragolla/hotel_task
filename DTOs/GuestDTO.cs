using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task.DTOs;

public record GuestDTO
{
    [JsonPropertyName("guest_id")]
     public int GuestId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("details")]
    public string Details { get; set; }
    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }
    [JsonPropertyName("schedule")]
 public List<ScheduleDTO> Schedule {get; internal set; }
    
}

public record GuestCreateDTO
{
    [JsonPropertyName("guest_id")]
     [Required]
     public int GuestId { get; set; }
    [JsonPropertyName("name")]
     [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [JsonPropertyName("details")]
     [Required]
    public string Details { get; set; }
    [JsonPropertyName("date")]
     [Required]
    public DateTimeOffset Date { get; set; }

    
}

public record GuestUpdateDTO
{
    
    [JsonPropertyName("name")]
     [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [JsonPropertyName("details")]
     [Required]
    public string Details { get; set; }
    [JsonPropertyName("date")]
     [Required]
    public DateTimeOffset Date { get; set; }

    
}