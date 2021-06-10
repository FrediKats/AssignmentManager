using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Repositories;
using AssignmentManager.Server.Services.Communication;

namespace AssignmentManager.Server.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SpecialityService(ISpecialityRepository specialityRepository, IUnitOfWork unitOfWork)
        {
            _specialityRepository = specialityRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Speciality>> ListAsync()
        {
            return await _specialityRepository.ListAsync();
        }

        public async Task<SaveSpecialityResponse> SaveAsync(Speciality speciality)
        {
            try
            {
                await _specialityRepository.AddAsync(speciality);
                await _unitOfWork.CompleteAsync();

                return new SaveSpecialityResponse(speciality);
            }
            catch (Exception ex)
            {
                return new SaveSpecialityResponse($"Error with saving the speciality: {ex.Message}");
            }
        }
    }
}