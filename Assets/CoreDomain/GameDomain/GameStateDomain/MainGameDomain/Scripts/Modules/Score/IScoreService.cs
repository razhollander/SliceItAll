namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score
{
    public interface IScoreService
    {
        void AddScore(int score);
        int PlayerScore { get; }
        void ResetScore();
    }
}