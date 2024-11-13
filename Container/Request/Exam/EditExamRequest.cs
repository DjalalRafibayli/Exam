namespace Container.Request.Exam
{
    public class EditExamRequest
    {
        public int Id { get; set; }
        public string LessonCode { get; set; }
        public int StudentNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public int Score { get; set; }
    }
}
