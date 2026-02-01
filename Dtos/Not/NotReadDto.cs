namespace StudentGradeApi.Dtos.Not;

public class NotReadDto
{
    public int Id { get; set; }

    public int OgrenciId { get; set; }
    public string OgrenciAdSoyad { get; set; } = string.Empty;

    public int DersId { get; set; }
    public string DersAdi { get; set; } = string.Empty;

    public int Vize { get; set; }
    public int Final { get; set; }
    public double Ortalama { get; set; }
}

