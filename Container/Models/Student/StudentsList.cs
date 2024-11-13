using Container.Models.Pagination;

namespace Container.Models.Student
{
    public class StudentsList : PaginationModel
    {
        public StudentsList()
        {
            this.Students = new List<StudentListGetModel>();
        }
        public List<StudentListGetModel> Students { get; set; }
    }
}
