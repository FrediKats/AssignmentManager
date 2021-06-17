using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class SpecialityResponse : BaseResponse
    {
        public SpecialityResponse(bool success, string message, Speciality speciality) : base(success, message)
        {
            Speciality = speciality;
        }

        public SpecialityResponse(Speciality speciality) : this(true, string.Empty, speciality)
        {
        }

        public SpecialityResponse(string message) : this(false, message, null)
        {
        }

        public Speciality Speciality { get; }
    }
}