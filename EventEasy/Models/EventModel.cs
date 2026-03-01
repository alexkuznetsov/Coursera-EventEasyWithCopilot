using System.ComponentModel.DataAnnotations;

namespace EventEasy.Models;

public class EventModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Location { get; set; } = string.Empty;
}