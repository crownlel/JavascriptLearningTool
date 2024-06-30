namespace JavascriptLearningTool.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int CorrectOption { get; set; }
        public int? SelectedOption { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int UserId { get; set; }
    }
}
