using Core.Entities;

namespace Core.DTOs.TeacherDTOs;
public class TeacherResponseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public Department? Department { get; set; }
    public string? Designation { get; set; }
    public string?  Image { get; set; } = null!;
}
