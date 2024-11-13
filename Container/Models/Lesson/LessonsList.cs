using Container.Models.Pagination;

namespace Container.Models.Lesson
{
    public class LessonsList : PaginationModel
    {
        public LessonsList()
        {
            this.Lessons = new List<LessonListGetModel>();
        }
        public List<LessonListGetModel> Lessons { get; set; }
    }
}
