using Container.Request.Lesson;
using Exam.Application.Repositories.Lessons;
using FluentValidation;

namespace Exam.Application.Validations.Lesson
{
    public class AddLessonRequestValidator : AbstractValidator<AddLessonRequest>
    {
        private readonly ILessonRepository _lessonRepository;
        public AddLessonRequestValidator(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
            RuleFor(x => x.TeacherLastName).NotNull().NotEmpty().WithMessage("TeacherLastName is required");
            RuleFor(x => x.TeacherName).NotNull().NotEmpty().WithMessage("TeacherName is required");
            RuleFor(x => x.Class).NotNull().NotEmpty().WithMessage("Class is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty()
                .WithMessage("Code is required")
                .Length(3)
                .MustAsync(async (code, cancellationToken) => await _lessonRepository.IsUniqueAsync(x => x.Code.ToLower().Trim(), code.ToLower().Trim()))
                .WithMessage("Code must be unique")
            ;
        }
    }
}
