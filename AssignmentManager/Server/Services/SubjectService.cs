using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Services
{
    public class SubjectService : ISubjectService
    {
        public Task<IReadOnlyCollection<Subject>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<Subject> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<Subject>> GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Post(Subject subject)
        {
            throw new System.NotImplementedException();
        }

        public void Put(Subject subject)
        {
            throw new System.NotImplementedException();
        }

        public Subject Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}