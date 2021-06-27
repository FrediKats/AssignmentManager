using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Shared;
using AutoMapper;

namespace AssignmentManager.Server.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Student, StudentResource>();
            CreateMap<Student, StudentResourceBriefly>();
            
            CreateMap<Group, GroupResource>();
            CreateMap<Group, GroupResourceBriefly>();
            CreateMap<Speciality, SpecialityResource>()
                .ForMember(dest => dest.StudyTypeDescription,
                    opt => opt.MapFrom(
                        src => src.StudyType.ToDescriptionString()
                        ));
            CreateMap<Speciality, SpecialityResourceBriefly>()
                .ForMember(dest => dest.StudyTypeDescription,
                    opt => opt.MapFrom(
                        src => src.StudyType.ToDescriptionString()
                    ));;
            CreateMap<Solution, SolutionResourceBriefly>();
            CreateMap<Solution, SolutionResource>();

            CreateMap<Assignment, AssignmentResourceBriefly>();
            CreateMap<Assignment, AssignmentResource>();
            
            CreateMap<Subject, SubjectResourceBriefly>();
            CreateMap<Subject, SubjectResource>();
            
            CreateMap<Instructor, InstructorResource>();
            CreateMap<Instructor, InstructorResourceBriefly>();

            CreateMap<InstructorSubject, InstructorSubjectResource>();
        }
    }
}