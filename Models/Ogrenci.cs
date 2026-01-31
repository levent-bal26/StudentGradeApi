namespace StudentGradeApi.Models;

public class Ogrenci
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Soyad { get; set; } = string.Empty;
    public string Numara { get; set; } = string.Empty;

    // Navigation
    public List<OgrenciDers> AldigiDersler { get; set; } = new();
}
