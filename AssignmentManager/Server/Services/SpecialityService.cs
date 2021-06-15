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

        public async Task<List<Speciality>> GetAll()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality> GetById(int id)
        {
            return await _context.Specialities.FindAsync(id);
        }

        public async Task<SaveSpecialityResponse> Create(Speciality item)
        {
            try
            {
                //check input value of EStudyType
                MappingHelper.EnumDescriptionToString(item.StudyType);
                await _context.Specialities.AddAsync(item);
                await _context.SaveChangesAsync();
                return new SaveSpecialityResponse(item);
            }
            catch (Exception er)
            {
                return new SaveSpecialityResponse(er.Message);
            }
        }

        public void Update(Speciality item)
        {
            throw new NotImplementedException();
        }

        public Speciality DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}