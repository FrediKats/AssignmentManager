using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services
{
    public interface ISpecialityService
    {
        Task<IEnumerable<Speciality>> ListAsync();
        void AddAsync(Speciality speciality);
    }
}