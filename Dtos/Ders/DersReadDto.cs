namespace StudentGradeApi.Dtos.Ders;

public class DersReadDto
{
    public int Id { get; set; }
    public string DersAdi { get; set; } = string.Empty;
    public int Kredi { get; set; }
}
