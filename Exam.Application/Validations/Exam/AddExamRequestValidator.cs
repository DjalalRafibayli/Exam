using Container.Request.Exam;
using FluentValidation;

namespace Exam.Application.Validations.Exam
{
    public class AddExamRequestValidator : AbstractValidator<AddExamRequest>
    {
        public AddExamRequestValidator()
        {
            RuleFor(x => x.LessonCode).NotNull().NotEmpty().WithMessage("LessonCode is required");
            RuleFor(x => x.StudentNumber).NotNull().NotEmpty().WithMessage("StudentNumber is required");
            RuleFor(x => x.ExamDate).NotNull().NotEmpty().WithMessage("ExamDate is required");
            RuleFor(x => x.Score).NotNull().NotEmpty().WithMessage("Score is required");
        }
    }
}
