using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
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
                .ForMember(opt => opt.StudyTypeName,
                    opt => opt.MapFrom(
                        src => MappingHelper.EnumDescriptionToString(src.StudyType))
                );
        }
    }
}