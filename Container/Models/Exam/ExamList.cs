using Container.Models.Pagination;

namespace Container.Models.Exam
{
    public class ExamList : PaginationModel
    {
        public ExamList()
        {
            this.Exams = new List<ExamListGetModel>();
        }
        public List<ExamListGetModel> Exams { get; set; }
    }
}
