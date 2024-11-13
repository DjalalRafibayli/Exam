using Container.Models.Exam;
using Container.Request.Exam;

namespace Exam.Application.Services.Exam
{
    public interface IExamService
    {
        Task AddExam(AddExamRequest Exam);
        Task UpdateExam(EditExamRequest Exam);
        Task DeleteExam(int Id);
        Task<ExamList> GetExams(int Page, int Limit);
        Task<ExamListGetModel> GetExam(int Id);
    }
}
