namespace Exam.Domain.DbModels;

public partial class Student
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Class { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
