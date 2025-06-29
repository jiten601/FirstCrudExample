using System.ComponentModel.DataAnnotations;

namespace FirstCrudExample.Models
{
    public class StudentInsert
    {
        [Required(ErrorMessage = "You must type student name.")]
        public string StdName { get; set; } = null!;

        public string? StdAddress { get; set; }
        [DataType(DataType.Date)]
        public DateOnly JoinDate { get; set; }

        public int FacultyId { get; set; }
    }
    public class StudentEdit : StudentInsert
    {
        public long StdId { get; set; }
    }
    public class StudentView : StudentEdit
    {
        public string FacultyName { get; set; } = null!;
        public static StudentEdit GetStudentEditView(Student s)
        {
            return new StudentEdit { StdId = s.StdId, StdName = s.StdName, StdAddress = s.StdAddress, FacultyId = s.FacultyId, JoinDate = s.JoinDate };
        }
    }
}