namespace StudentGradeApi.Models;

public class Ders
{
    public int Id { get; set; }
    public string DersAdi { get; set; } = string.Empty;
    public int Kredi { get; set; }

    // Navigation
    public List<OgrenciDers> AlanOgrenciler { get; set; } = new();
}
