

using System.Text.Json.Serialization;

namespace Core.Entities;
public class Department : BaseEntity
{
    public string Name { get; set; }

    public string? ShortName { get; set; } = null!;
    public Guid InstituteId { get; set; }
    public Institute? Institute { get; set; } = null!;
    [JsonIgnore]public ICollection<Course> Courses { get; set; } = new List<Course>();
    [JsonIgnore] public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    [JsonIgnore] public ICollection<Student> Students { get; set; } = new List<Student>();
}