namespace Container.Models.Exam
{
    public class ExamListGetModel
    {
        public int Id { get; set; }
        public string LessonCode { get; set; }
        public string LessonName { get; set; }
        public int StudentNumber { get; set; }
        public string StudentFullName { get; set; }
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
        public bool IsActive { get; set; }

    }
}
