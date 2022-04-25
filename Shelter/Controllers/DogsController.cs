using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shelter.Models;

namespace Shelter.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DogsController : ControllerBase
  {
    private readonly ShelterContext _db;
    public DogsController(ShelterContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination>> Get(string name, int age, string gender, string type, int page, int perPage)
    {
      IQueryable<Dog> query = _db.Dogs.AsQueryable();
      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }
      if (age != 0)
      {
        query = query.Where(entry => entry.Age == age);
      }
      if (gender != null)
      {
        query = query.Where(entry => entry.Gender == gender);
      }
      if (type != null)
      {
        query = query.Where(entry => entry.Type == type);
      }

      List<Dog> dogs = await query.ToListAsync();

      if (perPage == 0) perPage = 2;

      int total = dogs.Count;
      List<Dog> dogsPage = new List<Dog>();

      if (page < (total / perPage))
      {
        dogsPage = dogs.GetRange(page * perPage, perPage);
      }

      if (page == (total / perPage))
      {
        dogsPage = dogs.GetRange(page * perPage, total - (page * perPage));
      }

      return new Pagination()
      {
        DogSet = dogsPage,
        Total = total,
        PerPage = perPage,
        Page = page,
        PreviousPage = page == 0 ? $"/api/Dogs?page={page}" : $"/api/Dogs?page={page - 1}",
        NextPage = $"/api/Dogs?page={page + 1}",
      };
    }
    // GET: api/Dog/2
    [HttpGet("{id}")]
    public async Task<ActionResult<Dog>> GetDog(int id)
    {
      var dog = await _db.Dogs.FindAsync(id);

      if (dog == null)
      {
        return NotFound();
      }

      return dog;
    }

    // PUT: api/Dog/2
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Dog dog)
    {
      if (id != dog.DogId)
      {
        return BadRequest();
      }

      _db.Entry(dog).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DogExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Dog
    [HttpPost]
    public async Task<ActionResult<Dog>> Post(Dog dog)
    {
      _db.Dogs.Add(dog);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetDog), new { id = dog.DogId }, dog);
    }

    // DELETE: api/Dog/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDog(int id)
    {
      var dog = await _db.Dogs.FindAsync(id);
      if (dog == null)
      {
        return NotFound();
      }

      _db.Dogs.Remove(dog);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool DogExists(int id)
    {
      return _db.Dogs.Any(e => e.DogId == id);
    }
  }
}
