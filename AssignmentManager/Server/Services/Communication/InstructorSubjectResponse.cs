using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class InstructorSubjectResponse : BaseResponse
    {
        public InstructorSubjectResponse(bool success, string message, InstructorSubject instructorSubject) : base(success, message)
        {
            InstructorSubject = instructorSubject;
        }
        
        public InstructorSubjectResponse(InstructorSubject instructorSubject) : this(true, string.Empty, instructorSubject)
        {
        }

        public InstructorSubjectResponse(string message) : this(false, message, null)
        {
        }

        public InstructorSubject InstructorSubject { get; }
    }
}