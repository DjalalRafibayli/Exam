using Exam.Application.Filters;
using Exam.Application.Repositories.Exams;
using Exam.Application.Repositories.Lessons;
using Exam.Application.Repositories.Students;
using Exam.Application.Services.Exam;
using Exam.Application.Services.Lesson;
using Exam.Application.Services.Student;
using Exam.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Exam.Application.Extensions
{
    public static class ServicesExtension
    {
        public static void AddServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelStateAttribute>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });




            services.AddFluentValidationExtension();


            services.AddSingleton<ILessonRepository, LessonRepository>();
            services.AddSingleton<ILessonService, LessonService>();
            services.AddSingleton<IExamService, ExamService>();

            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddSingleton<IExamRepository, ExamRepository>();

            services.AddDbContext<ExamContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Context")), ServiceLifetime.Singleton);


        }
    }
}
