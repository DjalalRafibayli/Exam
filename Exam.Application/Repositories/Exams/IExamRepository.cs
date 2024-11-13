using Exam.Application.Repositories.Base;
using ExamEntity = Exam.Domain.DbModels.Exam;

namespace Exam.Application.Repositories.Exams
{
    public interface IExamRepository : IRepository<ExamEntity>
    {
    }
}
