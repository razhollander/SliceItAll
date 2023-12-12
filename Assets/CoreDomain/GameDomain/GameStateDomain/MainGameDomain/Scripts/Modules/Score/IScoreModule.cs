namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score
{
    public interface IScoreModule
    {
        void AddScore(float score);
        int PlayerScore { get; }
        void LoadScoreConfig();
        void StartCountingScore();
        void StopCountingScore();
        void SetMultiplier(bool isOn);
        void ResetScore();
    }
}