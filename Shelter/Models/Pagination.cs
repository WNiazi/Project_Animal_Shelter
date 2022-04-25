using System.Collections.Generic;

namespace Shelter.Models
{
  public class Pagination
  {
    public List<Cat> CatSet { get; set; }
    public List<Dog> DogSet { get; set; }
    public int Total { get; set; }
    public int PerPage { get; set; }
    public int Page { get; set; }
    public string PreviousPage { get; set; }
    public string NextPage { get; set; }
  }
}