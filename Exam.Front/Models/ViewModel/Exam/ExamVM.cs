using Container.Models.Lesson;
using Container.Models.Student;

namespace Exam.Front.Models.ViewModel.Exam
{
    public class ExamVM
    {
        public List<LessonSelectModel> lessons { get; set; }
        public List<StudentSelectModel> students { get; set; }
    }
}
