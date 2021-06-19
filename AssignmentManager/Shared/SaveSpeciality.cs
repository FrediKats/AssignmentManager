using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveSpeciality
    {
        [Required] [MaxLength(8)] public string Code { get; set; }

        [Required] public EStudyType EnumStudyType { get; set; }
    }
}