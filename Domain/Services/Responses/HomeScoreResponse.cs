namespace Domain.Services.Responses;

public class HomeScoreResponse
{
    public int TotalScore { get; set; }
    public int LastMonthScore { get; set; }
    public int PendingScore { get; set; }
}
