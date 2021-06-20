﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Services
{
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(AppDbContext context) : base(context)
        {
        }

        public Task<List<Subject>> GetAllSubjects()
        {
            return _context.Subjects.ToListAsync();
        }

        public async Task<SubjectResponse> SaveAsync(Subject subject)
        {
            try
            {
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                return new SubjectResponse(subject);
            }
            catch (Exception er)
            {
                return new SubjectResponse(er.Message);
            }
        }
    }
}