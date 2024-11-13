using Container.Models.Exam;
using Container.Request.Exam;
using Exam.Application.Repositories.Exams;
using Exam.Application.Validations.Exam;
using FluentValidation;

namespace Exam.Application.Services.Exam
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;

        public ExamService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task AddExam(AddExamRequest Exam)
        {
            var validate = new AddExamRequestValidator().Validate(Exam);
            if (!validate.IsValid)
            {
                throw new ValidationException(validate.Errors);
            }
            await _examRepository.AddAsync(new Domain.DbModels.Exam
            {
                ExamDate = DateOnly.FromDateTime(Exam.ExamDate),
                LessonCode = Exam.LessonCode,
                Score = Exam.Score,
                StudentNumber = Exam.StudentNumber,
                IsActive = true
            });
        }

        public async Task DeleteExam(int Id)
        {
            await _examRepository.DeleteAsync(await _examRepository.GetByIdAsync(Id));
        }

        public async Task<ExamListGetModel> GetExam(int Id)
        {
            var entity = await _examRepository.GetByIdAsync(Id);
            return new ExamListGetModel
            {
                Id = entity.Id,
                LessonCode = entity.LessonCode,
                StudentNumber = entity.StudentNumber,
                ExamDate = entity.ExamDate.ToDateTime(TimeOnly.MinValue),
                Score = entity.Score,
                IsActive = entity.IsActive
            };
        }

        public async Task<ExamList> GetExams(int Page, int Limit)
        {
            var exams = await _examRepository.GetAllAsync(filter: entity => entity.IsActive,
                                                    orderBy: query => query.OrderBy(e => e.ExamDate),
                                                    pageNumber: Page,
                                                    pageSize: Limit);
            return new ExamList
            {
                Exams = exams.Data.Select(entity => new ExamListGetModel
                {
                    Id = entity.Id,
                    LessonCode = entity.LessonCode,
                    StudentNumber = entity.StudentNumber,
                    ExamDate = entity.ExamDate.ToDateTime(TimeOnly.MinValue),
                    Score = entity.Score,
                    IsActive = entity.IsActive
                }).ToList(),
                activePage = Page,
                totalItemCount = exams.Pagination.totalItemCount,
                totalPageCount = exams.Pagination.totalPageCount
            };
        }

        public async Task UpdateExam(EditExamRequest Exam)
        {
            var validate = new EditExamRequestValidator().Validate(Exam);
            if (!validate.IsValid)
            {
                throw new ValidationException(validate.Errors);
            }

            var entity = await _examRepository.GetByIdAsync(Exam.Id);
            entity.ExamDate = DateOnly.FromDateTime(Exam.ExamDate);
            entity.LessonCode = Exam.LessonCode;
            entity.Score = Exam.Score;
            entity.StudentNumber = Exam.StudentNumber;
            await _examRepository.UpdateAsync(entity);
        }
    }
}
