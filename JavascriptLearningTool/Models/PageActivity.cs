namespace JavascriptLearningTool.Models
{
    public class PageActivity
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int PageId { get; set; }
        public DateTime Timestamp { get; set; }
        public int SecondsSpent { get; set; }

        public TimeSpan TimeSpent => TimeSpan.FromSeconds(SecondsSpent);
    }
}
