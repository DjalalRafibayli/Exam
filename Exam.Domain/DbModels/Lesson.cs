namespace Exam.Domain.DbModels;

public partial class Lesson
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Class { get; set; }

    public string TeacherName { get; set; } = null!;

    public string TeacherLastName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
