using Microsoft.EntityFrameworkCore;
using StudentGradeApi.Models;

namespace StudentGradeApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Sehir> Sehirler => Set<Sehir>();
    public DbSet<Bolum> Bolumler => Set<Bolum>();
    public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
    public DbSet<Ders> Dersler => Set<Ders>();
    public DbSet<OgrenciDers> OgrenciDersler => Set<OgrenciDers>();

}
