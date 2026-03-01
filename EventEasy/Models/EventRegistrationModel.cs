using System.ComponentModel.DataAnnotations;

namespace EventEasy.Models;

public class EventRegistrationModel
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Range(1, 10)] public int Tickets { get; set; } = 1;
}