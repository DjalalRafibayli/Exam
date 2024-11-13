namespace Container.Request.Exam
{
    public class AddExamRequest
    {
        public string LessonCode { get; set; }
        public int StudentNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
    }
}
