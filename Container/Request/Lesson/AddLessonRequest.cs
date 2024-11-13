namespace Container.Request.Lesson
{
    public class AddLessonRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Class { get; set; }
        public string TeacherName { get; set; }
        public string TeacherLastName { get; set; }
    }
}
