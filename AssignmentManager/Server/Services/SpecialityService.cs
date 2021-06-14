using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Mapping;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class SpecialityService : BaseService, ISpecialityService
    {
        public SpecialityService(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Speciality>> GetAllAsync()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality> GetByIdAsync(int id)
        {
            return await _context.Specialities.FindAsync(id);
        }

        public async Task<SaveSpecialityResponse> CreateAsync(Speciality item)
        {
            try
            {
                MappingHelper.StringValueOf(item.StudyType);
                await _context.Specialities.AddAsync(item);
                await _context.SaveChangesAsync();
                return new SaveSpecialityResponse(item);
            }
            catch (Exception er)
            {
                return new SaveSpecialityResponse(er.Message);
            }
        }

        public void UpdateAsync(Speciality item)
        {
            throw new NotImplementedException();
        }

        public Speciality DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}