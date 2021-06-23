using AssignmentManager.Server.Models;
using AssignmentManager.Shared;
using AutoMapper;

namespace AssignmentManager.Server.Mapping
{
    public class ResourceToModelProfiler : Profile
    {
        public ResourceToModelProfiler()
        {
            CreateMap<SaveSpecialityResource, Speciality>();
            CreateMap<SaveGroupResource, Group>();

            CreateMap<SaveInstructorResource, Instructor>();
            CreateMap<SaveSubjectResource, Subject>();
            CreateMap<SaveInstructorSubjectResource, InstructorSubjectResource>();

            CreateMap<SaveStudentResource, Student>();
            CreateMap<SaveSolutionResource, Solution>();
        }
    }
}