using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score
{
    public class ScoreModule : IScoreModule
    {
        private int _playerCurrentScore;
        public int PlayerScore => _playerCurrentScore;

        public ScoreModule()
        {
        }
        
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