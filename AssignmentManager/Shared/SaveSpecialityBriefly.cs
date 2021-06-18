using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveSpecialityBriefly
    {
        [Required] [MaxLength(8)] public string Code { get; set; }

        [Required] public EStudyType EnumStudyType { get; set; }
    }
}