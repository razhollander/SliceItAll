namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score
{
    public class ScoreService : IScoreService
    {
        private int _playerCurrentScore;
        public int PlayerScore => _playerCurrentScore;

        public void ResetScore()
        {
            _playerCurrentScore = 0;
        }

        public void AddScore(int score)
        {
            _playerCurrentScore += score;
        }
    }
}