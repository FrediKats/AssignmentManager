﻿using System.ComponentModel;

namespace AssignmentManager.Server.Models
{
    public enum EStudyType : byte
    {
        [Description("бакалавриат")] Bach = 1,
        [Description("магистратура")] Mast = 2,
        [Description("аспирантура")] Asp = 3,
        [Description("докторантура")] Doc = 5
    }
}