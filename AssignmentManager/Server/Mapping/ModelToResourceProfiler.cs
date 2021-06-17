using AssignmentManager.Server.Extensions;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

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
            CreateMap<Speciality, SpecialityResourceShort>()
                .ForMember(
                    opt => opt.StudyTypeName,
                    opt => opt.MapFrom(
                        src => src.EnumStudyType.ToDescriptionString())
                );
        }
    }
}