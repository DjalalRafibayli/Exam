using Container.Request.Lesson;
using FluentValidation;

namespace Exam.Application.Validations.Student
{
    public class EditLessonRequestValidator : AbstractValidator<EditLessonRequest>
    {
        public EditLessonRequestValidator()
        {
            RuleFor(x => x.TeacherLastName).NotNull().NotEmpty().WithMessage("TeacherLastName is required");
            RuleFor(x => x.TeacherName).NotNull().NotEmpty().WithMessage("TeacherName is required");
            RuleFor(x => x.Class).NotNull().NotEmpty().WithMessage("Class is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty()
                .WithMessage("Code is required")
                .Length(3);
        }
    }
}
