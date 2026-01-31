namespace StudentGradeApi.Models;

public class OgrenciDers
{
    public int Id { get; set; }

    public int OgrenciId { get; set; }
    public Ogrenci Ogrenci { get; set; } = null!;

    public int DersId { get; set; }
    public Ders Ders { get; set; } = null!;

    public int Vize { get; set; }
    public int Final { get; set; }

    public double Ortalama => Vize * 0.4 + Final * 0.6;
}
