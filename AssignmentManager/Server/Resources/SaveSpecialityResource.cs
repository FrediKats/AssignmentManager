using System.ComponentModel.DataAnnotations;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Resources
{
    public class SaveSpecialityResource
    {
        [Required] [MaxLength(8)] public string Code { get; set; }

        [Required] public EStudyType StudyType { get; set; }
    }
}