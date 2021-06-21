using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class SubjectResponse : BaseResponse
    {
        public SubjectResponse(bool success, string message, Subject subject) : base(success, message)
        {
            Subject = subject;
        }
        
        public SubjectResponse(Subject subject) : this(true, string.Empty, subject)
        {
        }

        public SubjectResponse(string message) : this(false, message, null)
        {
        }

        public Subject Subject { get; }
    }
}