namespace StudentGradeApi.Dtos.Ogrenci;

public class OgrenciReadDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Soyad { get; set; } = string.Empty;
    public string Numara { get; set; } = string.Empty;
}
