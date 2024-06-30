namespace JavascriptLearningTool.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Option1 { get; set; }
        public required string Option2 { get; set; }
        public required string Option3 { get; set; }
        public int CorrectOption { get; set; }
        public int CourseId { get; set; }
    }
}
