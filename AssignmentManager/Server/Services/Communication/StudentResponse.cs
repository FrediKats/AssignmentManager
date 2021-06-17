using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class StudentResponse : BaseResponse
    {
        public StudentResponse(bool success, string message, Student student) : base(success, message)
        {
            Student = student;
        }

        public StudentResponse(Student student) : this(true, string.Empty, student)
        {
        }

        public StudentResponse(string message) : this(false, message, null)
        {
        }

        public Student Student { get; }
    }
}