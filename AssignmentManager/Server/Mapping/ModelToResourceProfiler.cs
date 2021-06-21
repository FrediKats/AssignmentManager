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
            CreateMap<Group, GroupResource>();
            CreateMap<Speciality, SpecialityResource>()
                .ForMember(
                    opt => opt.StudyTypeName,
                    opt => opt.MapFrom(
                        src => src.EnumStudyType.ToDescriptionString())
                )
                .ForMember(
                    dist=> dist.EnumStudyType,
                    opt => opt.MapFrom(
                        src => src.EnumStudyType
                        )
                    );
            CreateMap<Instructor, InstructorResource>();
            CreateMap<Subject, SubjectResource>();

        }
    }
}