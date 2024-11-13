using Container.Models.Student;
using Container.Request.Student;
using Exam.Application.Repositories.Students;

namespace Exam.Application.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task AddStudent(AddStudentRequest Student)
        {
            await _studentRepository.AddAsync(new Domain.DbModels.Student
            {
                Name = Student.Name,
                LastName = Student.LastName,
                Class = Student.Class,
                Number = Student.Number,
                IsActive = true
            });
        }

        public async Task DeleteStudent(int Id)
        {
            await _studentRepository.DeleteAsync(await _studentRepository.GetByIdAsync(Id));
        }

        public async Task<StudentListGetModel> GetStudent(int Id)
        {
            var entity = await _studentRepository.GetByIdAsync(Id);
            return new StudentListGetModel
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                Class = entity.Class,
                Number = entity.Number,
                IsActive = entity.IsActive
            };
        }

        public async Task<StudentsList> GetStudents(int Page, int Limit)
        {
            var students = await _studentRepository.GetAllAsync(filter: entity => entity.IsActive,
                                                    orderBy: query => query.OrderBy(e => e.Name),
                                                    pageNumber: Page,
                                                    pageSize: Limit);
            return new StudentsList
            {
                Students = students.Data.Select(entity => new StudentListGetModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    LastName = entity.LastName,
                    Class = entity.Class,
                    Number = entity.Number,
                    IsActive = entity.IsActive
                }).ToList(),
                activePage = Page,
                totalItemCount = students.Pagination.totalItemCount,
                totalPageCount = students.Pagination.totalPageCount
            };
        }

        public async Task<List<StudentSelectModel>> GetStudentSelect()
        {
            var students = await _studentRepository.GetAllAsync(filter: entity => entity.IsActive,
                                                    orderBy: query => query.OrderBy(e => e.Name));
            return students.Data.Select(entity => new StudentSelectModel
            {
                Number = entity.Number,
            }).ToList();
        }

        public async Task UpdateStudent(EditStudentRequest Student)
        {
            var entity = await _studentRepository.GetByIdAsync(Student.Id);
            entity.Name = Student.Name;
            entity.LastName = Student.LastName;
            entity.Class = Student.Class;
            entity.Number = Student.Number;
            await _studentRepository.UpdateAsync(entity);
        }
    }
}
