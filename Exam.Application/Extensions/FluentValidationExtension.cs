using Exam.Application.Validations.Lesson;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Application.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidationExtension(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AddLessonRequestValidator>();
            //services.AddFluentValidationAutoValidation();
            //services.AddFluentValidationClientsideAdapters();
        }
    }
}
