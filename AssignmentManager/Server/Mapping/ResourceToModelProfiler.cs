using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AssignmentManager.Shared;
using AutoMapper;

namespace AssignmentManager.Server.Mapping
{
    public class ResourceToModelProfiler : Profile
    {
        public ResourceToModelProfiler()
        {
            CreateMap<SaveSpeciality, Speciality>();
            CreateMap<SaveGroupResource, Group>();

            CreateMap<SaveInstructorResource, Instructor>();
            CreateMap<SaveSubjectResource, Subject>();

            CreateMap<SaveStudentResource, Student>();
        }
    }
}