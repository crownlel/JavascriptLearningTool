namespace JavascriptLearningTool.Models
{
    public class QuestionProgress
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int TotalAnswers { get; set; }

        public int CorrectAnswers { get; set; }

        public double ProgressPercentage => TotalAnswers == 0 ? 0 : (double)CorrectAnswers / TotalAnswers * 100;

        public bool IsCompleted => ProgressPercentage >= _accuracyThreshold && TotalAnswers >= _minAttempts;

        private const int _minAttempts = 3;
        private const double _accuracyThreshold = 70.0;
    }
}
