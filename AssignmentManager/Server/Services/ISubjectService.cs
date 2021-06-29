using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Services
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetById(int id);
        Task<Subject> AddAsync(Subject subject);
        Task<Subject> UpdateAsync(int id, Subject subject);
        Task<Subject> DeleteAsync(int id);
    }
}