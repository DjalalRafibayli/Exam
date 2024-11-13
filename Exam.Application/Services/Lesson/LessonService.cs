using Container.Models.Lesson;
using Container.Request.Lesson;
using Exam.Application.Repositories.Lessons;
using Exam.Application.Validations.Exam;
using Exam.Application.Validations.Lesson;
using FluentValidation;

namespace Exam.Application.Services.Lesson
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task AddLesson(AddLessonRequest lesson)
        {
            var validator = new AddLessonRequestValidator(_lessonRepository);
            var validationResult = await validator.ValidateAsync(lesson);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            await _lessonRepository.AddAsync(new Domain.DbModels.Lesson
            {
                Name = lesson.Name,
                Class = lesson.Class,
                Code = lesson.Code,
                TeacherName = lesson.TeacherName,
                TeacherLastName = lesson.TeacherLastName,
            });
        }

        public async Task DeleteLesson(int Id)
        {
            var entity = await _lessonRepository.GetByIdAsync(Id);
            await _lessonRepository.DeleteAsync(entity);
        }

        public async Task<LessonListGetModel> GetLesson(int Id)
        {
            var entity = await _lessonRepository.GetByIdAsync(Id);
            return new LessonListGetModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Class = entity.Class,
                Code = entity.Code,
                TeacherName = entity.TeacherName,
                TeacherLastName = entity.TeacherLastName,
                IsActive = entity.IsActive
            };
        }

        public async Task<LessonsList> GetLessons(int Page, int Limit)
        {
            var lessons = await _lessonRepository.GetAllAsync(filter: entity => entity.IsActive,
                                                    orderBy: query => query.OrderBy(e => e.Name),
                                                    pageNumber: Page,
                                                    pageSize: Limit);
            return new LessonsList
            {
                Lessons = lessons.Data.Select(entity => new LessonListGetModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Class = entity.Class,
                    Code = entity.Code,
                    TeacherName = entity.TeacherName,
                    TeacherLastName = entity.TeacherLastName,
                    IsActive = entity.IsActive
                }).ToList(),
                activePage = Page,
                totalItemCount = lessons.Pagination.totalItemCount,
                totalPageCount = lessons.Pagination.totalPageCount
            };
        }

        public async Task<List<LessonSelectModel>> GetLessonSelect()
        {
            var lessons = await _lessonRepository.GetAllAsync(filter: entity => entity.IsActive,
                                                    orderBy: query => query.OrderBy(e => e.Name));
            return lessons.Data.Select(entity => new LessonSelectModel
            {
                Code = entity.Code,
            }).ToList();
        }

        public async Task UpdateLesson(EditLessonRequest lesson)
        {
            var entity = await _lessonRepository.GetByIdAsync(lesson.Id);
            entity.Name = lesson.Name;
            entity.Class = lesson.Class;
            entity.Code = lesson.Code;
            entity.TeacherName = lesson.TeacherName;
            entity.TeacherLastName = lesson.TeacherLastName;

            await _lessonRepository.UpdateAsync(entity);
        }
    }
}
