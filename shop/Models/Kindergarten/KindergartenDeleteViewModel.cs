namespace shop.Models.Kindergarten
{
    public class KindergartenDeleteViewModel
    {
        public Guid id { get; set; }
        public string? GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
