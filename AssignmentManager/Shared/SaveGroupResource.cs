using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveGroupResource
    {
        [Required] [MaxLength(8)] public string Name { get; set; }
        public int? SpecialityId { get; set; }
    }
}