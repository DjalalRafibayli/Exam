using Container.Models.Student;
using Container.Request.Student;

namespace Exam.Application.Services.Student
{
    public interface IStudentService
    {
        Task AddStudent(AddStudentRequest Student);
        Task UpdateStudent(EditStudentRequest Student);
        Task DeleteStudent(int Id);
        Task<StudentsList> GetStudents(int Page, int Limit);
        Task<StudentListGetModel> GetStudent(int Id);
        Task<List<StudentSelectModel>> GetStudentSelect();
    }
}
