using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Services
{
    public interface ISubjectService
    {
        public Task<IReadOnlyCollection<Subject>> Get();

        public Task<Subject> GetById(int id);

        public Task<IReadOnlyCollection<Subject>> GetByName(string name);

        public void Post(Subject subject);

        public void Put(Subject subject);

        public Subject Delete(int id);
    }
}