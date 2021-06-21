namespace AssignmentManager.Server.Resources
{
    public class StudentResource
    {
        public int IsuId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public GroupResourceBriefly Group { get; set; }
    }
}