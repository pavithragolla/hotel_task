using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace task.DTOs;


public record StaffDTO
{
    [JsonPropertyName("staff_id")]
    public int StaffId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
}
public record StaffCreateDTO
{
    [JsonPropertyName("staff_id")]
    [Required]
    public int StaffId { get; set; }
    [JsonPropertyName("name")]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }
    [JsonPropertyName("address")]
    [Required]
    public string Address { get; set; }
    [JsonPropertyName("gender")]
    [Required]
    public string Gender { get; set; }
}

public record StaffUpdateDTO
{
   
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
}