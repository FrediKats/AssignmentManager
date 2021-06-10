using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services.Communication
{
    public class SaveSpecialityResponse : BaseResponse
    {
        public Speciality Speciality { get; private set; }
        public SaveSpecialityResponse(bool success, string message, Speciality speciality) : base(success, message)
        {
            Speciality = speciality;
        }

        public SaveSpecialityResponse(Speciality speciality) : this(true, string.Empty, speciality) { }
        
        public SaveSpecialityResponse(string message) : this(true, message, null) { }
    }
}