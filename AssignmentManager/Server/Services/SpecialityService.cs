using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Repositories;

namespace AssignmentManager.Server.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;

        public SpecialityService(ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository;
        }
        public async Task<IEnumerable<Speciality>> ListAsync()
        {
            return await _specialityRepository.ListAsync();
        }
    }
}