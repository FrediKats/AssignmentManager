using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class InstructorResponse : BaseResponse
    {
        public InstructorResponse(bool success, string message, Instructor instructor) : base(success, message)
        {
            Instructor = instructor;
        }
        
        public InstructorResponse(Instructor instructor) : this(true, string.Empty, instructor)
        {
        }

        public InstructorResponse(string message) : this(false, message, null)
        {
        }

        public Instructor Instructor { get; }
    }
}