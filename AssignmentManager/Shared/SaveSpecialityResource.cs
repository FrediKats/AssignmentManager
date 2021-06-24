using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveSpecialityResource
    {
        [Required] [MaxLength(8)] public string Code { get; set; }

        [Required] public byte EnumStudyType { get; set; }
        [Required] public int[] SubjectsId { get; set; }
    }
}