using System.ComponentModel.DataAnnotations;

namespace StudentGradeApi.Dtos.Ogrenci;

public class OgrenciCreateDto
{
    [Required, MaxLength(50)]
    public string Ad { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Soyad { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Numara { get; set; } = string.Empty;
}
