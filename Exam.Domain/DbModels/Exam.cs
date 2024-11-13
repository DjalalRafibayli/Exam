namespace Exam.Domain.DbModels;

public partial class Exam
{
    public int Id { get; set; }

    public string LessonCode { get; set; } = null!;

    public int StudentNumber { get; set; }

    public DateOnly ExamDate { get; set; }

    public int Score { get; set; }

    public bool IsActive { get; set; }

    public virtual Lesson LessonCodeNavigation { get; set; } = null!;

    public virtual Student StudentNumberNavigation { get; set; } = null!;
}
