using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeApi.Data;
using StudentGradeApi.Dtos.Ogrenci;
using StudentGradeApi.Models;

namespace StudentGradeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OgrencilerController : ControllerBase
{
    private readonly AppDbContext _context;

    public OgrencilerController(AppDbContext context)
    {
        _context = context;
    }

    // ============================
    // POST: api/ogrenciler
    // ============================
    [HttpPost]
    public async Task<IActionResult> Create(OgrenciCreateDto dto)
    {
        var ogrenci = new Ogrenci
        {
            Ad = dto.Ad,
            Soyad = dto.Soyad,
            Numara = dto.Numara
        };

        _context.Ogrenciler.Add(ogrenci);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = ogrenci.Id },
            new OgrenciReadDto
            {
                Id = ogrenci.Id,
                Ad = ogrenci.Ad,
                Soyad = ogrenci.Soyad,
                Numara = ogrenci.Numara
            });
    }

    // ============================
    // GET: api/ogrenciler
    // ============================
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OgrenciListDto>>> GetAll()
    {
        var ogrenciler = await _context.Ogrenciler
            .Select(o => new OgrenciListDto
            {
                Id = o.Id,
                AdSoyad = o.Ad + " " + o.Soyad,
                Numara = o.Numara
            })
            .ToListAsync();

        return Ok(ogrenciler);
    }

    // ============================
    // GET: api/ogrenciler/{id}
    // ============================
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OgrenciReadDto>> GetById(int id)
    {
        var ogrenci = await _context.Ogrenciler.FindAsync(id);

        if (ogrenci is null)
            return NotFound();

        return Ok(new OgrenciReadDto
        {
            Id = ogrenci.Id,
            Ad = ogrenci.Ad,
            Soyad = ogrenci.Soyad,
            Numara = ogrenci.Numara
        });
    }

    // ============================
    // PUT: api/ogrenciler/{id}
    // ============================
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, OgrenciUpdateDto dto)
    {
        var ogrenci = await _context.Ogrenciler.FindAsync(id);

        if (ogrenci is null)
            return NotFound();

        ogrenci.Ad = dto.Ad;
        ogrenci.Soyad = dto.Soyad;
        ogrenci.Numara = dto.Numara;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
