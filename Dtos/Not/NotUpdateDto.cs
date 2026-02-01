using System.ComponentModel.DataAnnotations;

namespace StudentGradeApi.Dtos.Not;

public class NotUpdateDto
{
    [Range(0, 100)]
    public int Vize { get; set; }

    [Range(0, 100)]
    public int Final { get; set; }
}
