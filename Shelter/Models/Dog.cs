using System.ComponentModel.DataAnnotations;

namespace Shelter.Models
{
  public class Dog
  {
    public int DogId { get; set; }
    [Required]
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Type { get; set; }

  }
}