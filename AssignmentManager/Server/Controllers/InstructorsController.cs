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
    public class InstructorsController : ControllerBase
    {
        private readonly IMapper _mapper;
    }
}