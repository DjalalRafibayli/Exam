using Exam.Application.Repositories.Base;
using Exam.Domain.DbModels;
using Microsoft.Extensions.Logging;

namespace Exam.Application.Repositories.Lessons
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(ExamContext context, ILogger<BaseRepository<Lesson>> logger) : base(context, logger)
        {
        }
    }
}
