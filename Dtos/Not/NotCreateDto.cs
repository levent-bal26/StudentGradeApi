using System.ComponentModel.DataAnnotations;

namespace StudentGradeApi.Dtos.Not;

public class NotCreateDto
{
    [Required]
    public int OgrenciId { get; set; }

    [Required]
    public int DersId { get; set; }

    [Range(0, 100)]
    public int Vize { get; set; }

    [Range(0, 100)]
    public int Final { get; set; }
}
