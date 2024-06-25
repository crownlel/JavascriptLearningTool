namespace JavascriptLearningTool.Models
{
    public class UserProgress
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int LastPage { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
