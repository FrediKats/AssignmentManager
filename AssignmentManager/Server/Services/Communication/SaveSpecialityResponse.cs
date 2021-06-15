using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class SaveSpecialityResponse : BaseResponse
    {
        public SaveSpecialityResponse(bool success, string message, Speciality speciality) : base(success, message)
        {
            Speciality = speciality;
        }

        public SaveSpecialityResponse(Speciality speciality) : this(true, string.Empty, speciality)
        {
        }

        public SaveSpecialityResponse(string message) : this(false, message, null)
        {
        }

        public Speciality Speciality { get; }
    }
}