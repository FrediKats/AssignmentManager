using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class InstructorsResponse : BaseResponse
    {
        public InstructorsResponse(bool success, string message, Instructor instructor) : base(success, message)
        {
            Instructor = instructor;
        }
        
        public InstructorsResponse(Instructor instructor) : this(true, string.Empty, instructor)
        {
        }

        public InstructorsResponse(string message) : this(false, message, null)
        {
        }

        public Instructor Instructor { get; }
    }
}