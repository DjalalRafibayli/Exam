using Container.Models.Lesson;
using Container.Request.Lesson;

namespace Exam.Application.Services.Lesson
{
    public interface ILessonService
    {
        Task AddLesson(AddLessonRequest lesson);
        Task UpdateLesson(EditLessonRequest lesson);
        Task DeleteLesson(int Id);
        Task<LessonsList> GetLessons(int Page, int Limit);
        Task<LessonListGetModel> GetLesson(int Id);
        Task<List<LessonSelectModel>> GetLessonSelect();
    }
}
