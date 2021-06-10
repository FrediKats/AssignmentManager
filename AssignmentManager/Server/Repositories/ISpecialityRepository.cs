using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;

namespace AssignmentManager.Server.Repositories
{
    public interface ISpecialityRepository
    {
        Task<IEnumerable<Speciality>> ListAsync();
        Task AddAsync(Speciality speciality);
    }
}