using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface ISpecialityService
    {
        Task<IEnumerable<Speciality>> ListAsync();
        Task<SaveSpecialityResponse> SaveAsync(Speciality speciality);
    }
}