using Exam.Application.Repositories.Base;
using Exam.Domain.DbModels;
using Microsoft.Extensions.Logging;
using ExamEntity = Exam.Domain.DbModels.Exam;

namespace Exam.Application.Repositories.Exams
{
    public class ExamRepository : BaseRepository<ExamEntity>, IExamRepository
    {
        public ExamRepository(ExamContext context, ILogger<BaseRepository<ExamEntity>> logger) : base(context, logger)
        {
        }
    }
}
