using Container.Request.Lesson;
using Container.Request.Student;
using FluentValidation;

namespace Exam.Application.Validations.Student
{
    public class EditStudentRequestValidator : AbstractValidator<EditStudentRequest>
    {
        public EditStudentRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Number).NotNull().NotEmpty().WithMessage("Number is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.Class).NotNull().NotEmpty().WithMessage("Class is required");
        }
    }
}
