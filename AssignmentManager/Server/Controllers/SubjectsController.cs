using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Services;
using IdentityModel.Client;

namespace AssignmentManager.Server.Controllers
{
    public class SubjectsController
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        
        [HttpGet]
        public IReadOnlyCollection<Subject> Get()
        {
            //TODO: return all existing subjects
            var result = _subjectService.Get().Result;
            return result;
        }

        [HttpGet("{id:int}")]
        public ActionResult<string> GetById(int id)
        {
            //TODO: return subject by id as json
            throw new NotImplementedException();
        }

        [HttpGet("{name:string}")]
        public IReadOnlyCollection<string> GetByName(string name)
        {
            //TODO: return all subjects with name as substring
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public void Post([FromBody] string value)
        { 
            //TODO: add new subject to db
            throw new NotImplementedException();
        }

        // PUT api/values/5
        [HttpPut("{id:int}")]
        public void Put(int id, [FromBody] string value)
        {   
            //TODO: update subject in db
            throw new NotImplementedException();
        }
        
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {  
            //TODO: delete subject from db
            throw new NotImplementedException();
        }
    }
}