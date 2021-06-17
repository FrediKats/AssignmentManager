﻿using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Resources
{
    public class SpecialityResourceShort
    {
        public int Id { get; set; }
        public string StudyTypeName { get; set; }
        public EStudyType EnumStudyType { get; set; }
        public string Code { get; set; }
    }
}