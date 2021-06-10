using System;
using System.ComponentModel;
using System.Reflection;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManager.Server.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Student, StudentResource>();
            CreateMap<Group, GroupResource>();
            CreateMap<Speciality, SpecialityResource>()
                .ForMember(opt => opt.StudyType,
                    opt => opt.MapFrom(
                        src => StringValueOf(src.StudyType))
                        );
        }
        
        //Show descriptions for enums
        private static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
    
}