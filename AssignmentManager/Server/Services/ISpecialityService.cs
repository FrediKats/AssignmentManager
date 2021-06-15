using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface ISpecialityService
    {
        Task<List<Speciality>> GetAll();
        Task<Speciality> GetById(int id);
        Task<SaveSpecialityResponse> Create(Speciality item);
        void Update(Speciality item);
        Speciality DeleteById(int id);
    }
}