namespace JavascriptLearningTool.Models
{
    public class Course
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}
