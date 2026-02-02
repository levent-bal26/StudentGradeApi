using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using StudentGradeApi.Data;

using StudentGradeApi.Dtos.Ders;

using StudentGradeApi.Models;

namespace StudentGradeApi.Controllers;

[ApiController]

[Route("api/[controller]")]

public class DerslerController : ControllerBase

{

private readonly AppDbContext _context;

public DerslerController(AppDbContext context)

{

_context = context;

}

// POST: api/dersler

[HttpPost]

public async Task<IActionResult> Create(DersCreateDto dto)

{

var ders = new Ders

{

DersAdi = dto.DersAdi,

Kredi = dto.Kredi

};

_context.Dersler.Add(ders);

await _context.SaveChangesAsync();

var readDto = new DersReadDto

{

Id = ders.Id,

DersAdi = ders.DersAdi,

Kredi = ders.Kredi

};

return CreatedAtAction(nameof(GetById), new { id = ders.Id }, readDto);

}

// GET: api/dersler

[HttpGet]

public async Task<ActionResult<IEnumerable<DersReadDto>>> GetAll()

{

var dersler = await _context.Dersler

.Select(d => new DersReadDto

{

Id = d.Id,

DersAdi = d.DersAdi,

Kredi = d.Kredi

})

.ToListAsync();

return Ok(dersler);

}

// GET: api/dersler/{id}

[HttpGet("{id:int}")]

public async Task<ActionResult<DersReadDto>> GetById(int id)

{

var ders = await _context.Dersler.FindAsync(id);

if (ders is null)

return NotFound();

return Ok(new DersReadDto

{

Id = ders.Id,

DersAdi = ders.DersAdi,

Kredi = ders.Kredi

});

}

}