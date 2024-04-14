
using System.ComponentModel.DataAnnotations;

namespace Notes.Entities;

public class Note
{
    [Key]
    public int Rowid { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}