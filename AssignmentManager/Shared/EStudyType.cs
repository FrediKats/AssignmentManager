using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AssignmentManager.Shared
{
    public enum EStudyType : byte
    {
        //[EnumMember(Value = "Awaiting Approval")]
        [Description("бакалавриат")] Bachelor = 1,
        [Description("магистратура")] Master = 2,
        [Description("аспирантура")] Postgraduate = 3,
        [Description("докторантура")] Doctorate = 4

    }
}