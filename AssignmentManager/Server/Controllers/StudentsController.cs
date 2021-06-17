using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Controllers
{
    [Route("/api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _service;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _service = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IReadOnlyCollection<StudentResourceBriefly> GetAllStudents()
        {
            var students = _service.GetAll().Result;
            var resources = _mapper.Map<List<Student>, List<StudentResourceBriefly>>(students);
            return resources;
        }
    }
}