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
  public class CatsController : ControllerBase
  {
    private readonly ShelterContext _db;
    public CatsController(ShelterContext db)
    {
      _db = db;
    }


    [HttpGet]
    public async Task<ActionResult<Pagination>> Get(string name, int age, string gender, string type, int page, int perPage)
    {
      IQueryable<Cat> query = _db.Cats.AsQueryable();
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

      List<Cat> cats = await query.ToListAsync();

      if (perPage == 0) perPage = 2;

      int total = cats.Count;
      List<Cat> catsPage = new List<Cat>();

      if (page < (total / perPage))
      {
        catsPage = cats.GetRange(page * perPage, perPage);
      }

      if (page == (total / perPage))
      {
        catsPage = cats.GetRange(page * perPage, total - (page * perPage));
      }

      return new Pagination()
      {
        CatSet = catsPage,
        Total = total,
        PerPage = perPage,
        Page = page,
        PreviousPage = page == 0 ? $"/api/Cats?page={page}" : $"/api/Cats?page={page - 1}",
        NextPage = $"/api/Cats?page={page + 1}",
      };
    }
    // GET: api/Cat/2
    [HttpGet("{id}")]
    public async Task<ActionResult<Cat>> GetCat(int id)
    {
      var cat = await _db.Cats.FindAsync(id);

      if (cat == null)
      {
        return NotFound();
      }

      return cat;
    }

    // PUT: api/Cat/2
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Cat cat)
    {
      if (id != cat.CatId)
      {
        return BadRequest();
      }

      _db.Entry(cat).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CatExists(id))
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

    // POST: api/Cat
    [HttpPost]
    public async Task<ActionResult<Cat>> Post(Cat cat)
    {
      _db.Cats.Add(cat);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCat), new { id = cat.CatId }, cat);
    }

    // DELETE: api/Cat/2
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCat(int id)
    {
      var cat = await _db.Cats.FindAsync(id);
      if (cat == null)
      {
        return NotFound();
      }

      _db.Cats.Remove(cat);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool CatExists(int id)
    {
      return _db.Cats.Any(e => e.CatId == id);
    }
  }
}
