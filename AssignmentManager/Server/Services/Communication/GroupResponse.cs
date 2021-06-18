using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class GroupResponse : BaseResponse
    {
        public GroupResponse(bool success, string message, Group group) : base(success, message)
        {
            Group = group;
        }

        public GroupResponse(Group group) : this(true, string.Empty, group)
        {
        }

        public GroupResponse(string message) : this(false, message, null)
        {
        }

        public Group Group { get; }
    }
}