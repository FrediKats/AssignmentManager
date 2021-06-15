using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class SaveGroupResponse : BaseResponse
    {
        public SaveGroupResponse(bool success, string message, Group group) : base(success, message)
        {
            Group = group;
        }

        public SaveGroupResponse(Group group) : this(true, string.Empty, group)
        {
        }

        public SaveGroupResponse(string message) : this(false, message, null)
        {
        }

        public Group Group { get; }
    }
}