using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Services
{
    public interface ISpecialityService
    {
        Task<List<Speciality>> GetAll();
        Task<Speciality> GetById(int id);
        Task<Speciality> Create(SaveSpecialityResource item);
        Task<Speciality> Update(int id, SaveSpecialityResource item);
        Task<Speciality> DeleteById(int id);
    }
}