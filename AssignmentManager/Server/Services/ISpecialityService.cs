using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface ISpecialityService
    {
        Task<List<Speciality>> GetAllAsync();
        Task<Speciality> GetByIdAsync(int id);
        Task<SaveSpecialityResponse> CreateAsync(Speciality item);
        void UpdateAsync(Speciality item);
        Speciality DeleteByIdAsync(int id);
    }
}