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
                .ForMember(
                    opt => opt.StudyTypeName,
                    opt => opt.MapFrom(
                        src => src.EnumStudyType.ToDescriptionString())
                );
            CreateMap<Speciality,SpecialityResourceBriefly>()
                .ForMember(
                    opt => opt.StudyTypeName,
                    opt => opt.MapFrom(
                        src => src.EnumStudyType.ToDescriptionString())
                );
            
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