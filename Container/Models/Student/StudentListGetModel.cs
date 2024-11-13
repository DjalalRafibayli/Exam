namespace Container.Models.Student
{
    public class StudentListGetModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Class { get; set; }
        public bool IsActive { get; set; }
    }
}
