namespace Container.Models.Lesson
{
    public class LessonListGetModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Class { get; set; }

        public string TeacherName { get; set; } = null!;

        public string TeacherLastName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
