namespace WebApplication1.Models
{
    public class UserStatistic
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Identyfikator użytkownika
        public int ExerciseTypeId { get; set; }
        public int SessionsInLastFourWeeks { get; set; }
        public int BestResult { get; set; }
    }
}
