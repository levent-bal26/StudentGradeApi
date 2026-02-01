using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeApi.Data;
using StudentGradeApi.Dtos.Not;
using StudentGradeApi.Models;

namespace StudentGradeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotlarController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotlarController(AppDbContext context)
    {
        _context = context;
    }

    // ============================
    // POST: api/notlar
    // Öğrenciyi derse kaydet + ilk notları gir
    // ============================
    [HttpPost]
    public async Task<IActionResult> Create(NotCreateDto dto)
    {
        // Öğrenci var mı?
        var ogrenciVarMi = await _context.Ogrenciler.AnyAsync(o => o.Id == dto.OgrenciId);
        if (!ogrenciVarMi)
            return BadRequest($"Ogrenci bulunamadi. OgrenciId={dto.OgrenciId}");

        // Ders var mı?
        var dersVarMi = await _context.Dersler.AnyAsync(d => d.Id == dto.DersId);
        if (!dersVarMi)
            return BadRequest($"Ders bulunamadi. DersId={dto.DersId}");

        // Aynı öğrenci aynı derse daha önce kaydolmuş mu? (Çift kayıt engeli)
        var varOlanKayit = await _context.OgrenciDersler
            .AnyAsync(x => x.OgrenciId == dto.OgrenciId && x.DersId == dto.DersId);

        if (varOlanKayit)
            return Conflict("Bu ogrenci bu derse zaten kayitli.");

        var kayit = new OgrenciDers
        {
            OgrenciId = dto.OgrenciId,
            DersId = dto.DersId,
            Vize = dto.Vize,
            Final = dto.Final
        };

        _context.OgrenciDersler.Add(kayit);
        await _context.SaveChangesAsync();

        // Detay döndürmek için ilişkili alanları dolduralım
        var read = await _context.OgrenciDersler
            .Include(x => x.Ogrenci)
            .Include(x => x.Ders)
            .Where(x => x.Id == kayit.Id)
            .Select(x => new NotReadDto
            {
                Id = x.Id,

                OgrenciId = x.OgrenciId,
                OgrenciAdSoyad = x.Ogrenci.Ad + " " + x.Ogrenci.Soyad,

                DersId = x.DersId,
                DersAdi = x.Ders.DersAdi,

                Vize = x.Vize,
                Final = x.Final,
                Ortalama = x.Ortalama
            })
            .FirstAsync();

        return CreatedAtAction(nameof(GetById), new { id = read.Id }, read);
    }

    // ============================
    // GET: api/notlar
    // Tüm notları listele (öğrenci + ders + ortalama)
    // ============================
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotReadDto>>> GetAll()
    {
        var notlar = await _context.OgrenciDersler
            .Include(x => x.Ogrenci)
            .Include(x => x.Ders)
            .Select(x => new NotReadDto
            {
                Id = x.Id,

                OgrenciId = x.OgrenciId,
                OgrenciAdSoyad = x.Ogrenci.Ad + " " + x.Ogrenci.Soyad,

                DersId = x.DersId,
                DersAdi = x.Ders.DersAdi,

                Vize = x.Vize,
                Final = x.Final,
                Ortalama = x.Ortalama
            })
            .ToListAsync();

        return Ok(notlar);
    }

    // ============================
    // GET: api/notlar/{id}
    // Tek bir not kaydı
    // ============================
    [HttpGet("{id:int}")]
    public async Task<ActionResult<NotReadDto>> GetById(int id)
    {
        var not = await _context.OgrenciDersler
            .Include(x => x.Ogrenci)
            .Include(x => x.Ders)
            .Where(x => x.Id == id)
            .Select(x => new NotReadDto
            {
                Id = x.Id,

                OgrenciId = x.OgrenciId,
                OgrenciAdSoyad = x.Ogrenci.Ad + " " + x.Ogrenci.Soyad,

                DersId = x.DersId,
                DersAdi = x.Ders.DersAdi,

                Vize = x.Vize,
                Final = x.Final,
                Ortalama = x.Ortalama
            })
            .FirstOrDefaultAsync();

        if (not is null)
            return NotFound();

        return Ok(not);
    }

    // ============================
    // PUT: api/notlar/{id}
    // Vize/Final güncelle (NotUpdateDto)
    // ============================
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, NotUpdateDto dto)
    {
        var kayit = await _context.OgrenciDersler.FindAsync(id);

        if (kayit is null)
            return NotFound();

        kayit.Vize = dto.Vize;
        kayit.Final = dto.Final;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
