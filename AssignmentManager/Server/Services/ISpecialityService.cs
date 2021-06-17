using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public interface ISpecialityService
    {
        Task<List<Speciality>> GetAll();
        Task<SpecialityResponse> GetById(int id);
        Task<SpecialityResponse> Create(Speciality item);
        Task<SpecialityResponse> Update(int id, Speciality item);
        Task<SpecialityResponse> DeleteById(int id);
        Task<SpecialityResponse> DeleteCascadeById(int id);
    }
}