using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;
public class Teacher : BaseEntity
{
    public string? Name { get; set; } = null;
    public string Email { get; set; }
    public string? Role { get; set; } = "Teacher";
    public string Password { get; set; }
    public Guid DepartmentId { get; set; }
    public Department? Department { get; set; } = null;
    public string? BankAccount { get; set; } = null!;
    public string Designation { get; set; } = "Lecturer";
    public string? Image { get; set; } = null!;
    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    // [ForeignKey("Chairman")]
    
    public ICollection<Exam> ChairmanOfExams { get; set; } = new List<Exam>();
    public ICollection<Exam> ChiefInvigilatorOfExams { get; set; } = new List<Exam>();
    public ICollection<TermPaperResponsibilities> SupervisorOfTermPapers { get; set; } = new List<TermPaperResponsibilities>();
    public ICollection<TheoryCourseResponsibles> QuestionSettersOfCourses { get; set; } = new List<TheoryCourseResponsibles>();

    public ICollection<TermPaperResponsibilities> ExaminerOfTermPapers { get; set; } = new List<TermPaperResponsibilities>();

}