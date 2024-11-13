using Container.Models.Exam;
using Container.Models.Lesson;
using Container.Models.Student;

namespace Exam.Front.Models.ViewModel.Exam
{
    public class ExamEditVM
    {
        public ExamListGetModel exam { get; set; }
        public List<LessonSelectModel> lessons { get; set; }
        public List<StudentSelectModel> students { get; set; }
    }
}
