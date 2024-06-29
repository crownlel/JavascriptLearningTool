namespace JavascriptLearningTool.Models
{
    public class Test
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Duration { get; set; }
        public bool IsPublished { get; set; }
        public int CourseId { get; set; }
    }
}
