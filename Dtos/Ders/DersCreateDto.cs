using System.ComponentModel.DataAnnotations;

namespace StudentGradeApi.Dtos.Ders;

public class DersCreateDto
{
    [Required, MaxLength(100)]
    public string DersAdi { get; set; } = string.Empty;

    [Range(0, 30)]
    public int Kredi { get; set; }
}
