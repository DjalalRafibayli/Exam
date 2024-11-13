using Exam.Application.Repositories.Base;
using Exam.Domain.DbModels;
using Microsoft.Extensions.Logging;

namespace Exam.Application.Repositories.Students
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ExamContext context, ILogger<BaseRepository<Student>> logger) : base(context, logger)
        {
        }
    }
}
