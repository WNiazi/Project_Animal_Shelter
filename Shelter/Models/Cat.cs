using System.ComponentModel.DataAnnotations;

namespace Shelter.Models
{
  public class Cat
  {
    public int CatId { get; set; }
    [Required]
    public string Name { get; set; }
    // [Required]
    // [Range(0, 10, ErrorMessage = "Age must be between 0 and 100.")]
    public int Age { get; set; }
    // [Required]
    public string Gender { get; set; }
    // [Required]
    public string Type { get; set; }
  }
}