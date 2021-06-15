using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class SaveStudentResponse : BaseResponse
    {
        public SaveStudentResponse(bool success, string message, Student student) : base(success, message)
        {
            Student = student;
        }

        public SaveStudentResponse(Student student) : this(true, string.Empty, student)
        {
        }

        public SaveStudentResponse(string message) : this(false, message, null)
        {
        }

        public Student Student { get; }
    }
}