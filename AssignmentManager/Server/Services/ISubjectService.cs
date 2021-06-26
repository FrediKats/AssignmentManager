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
        Task<SubjectResponse> GetById(int id);
        Task<SubjectResponse> SaveAsync(Subject subject);
        Task<SubjectResponse> UpdateAsync(int id, Subject subject);
        Task<SubjectResponse> DeleteAsync(int id);
    }
}