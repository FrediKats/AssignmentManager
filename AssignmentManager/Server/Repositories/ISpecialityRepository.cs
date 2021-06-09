using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Repositories
{
    public interface ISpecialityRepository
    {
        Task<IEnumerable<Speciality>> ListAsync();
        void AddAsync(Speciality speciality);
    }
}