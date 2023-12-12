using System;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiView : MonoBehaviour
    {
        [SerializeField] private Countable _scoreCountable;
        [SerializeField] private Countable _highScoreCountable;
        [SerializeField] private Countable _timePlayingCountable;
        [SerializeField] private Countable _asteroidsPassedCountable;
        [SerializeField] private GameObject _inGamePanel;
        [SerializeField] private GameObject _beforeGamePanel;
        [SerializeField] private GameOverUiView _gameOverView;
        
        private Action _onPlayAgainClicked;

        public void Setup(Action onPlayAgainClicked)
        {
            _onPlayAgainClicked = onPlayAgainClicked;
        }

        public void ShowGameOverPanel(int score,
            int timePlayed,
            int asteroidsPassed,
            bool isNewHighScore,
            int highScore)
        {
            _gameOverView.SetAllTexts(score, timePlayed, asteroidsPassed, isNewHighScore, highScore);
            _gameOverView.gameObject.SetActive(true);
        }
        
        public void HideGameOverPanel()
        {
            _gameOverView.gameObject.SetActive(false);
        }
        
        // called from button clicked
        public void OnPlayAgainClicked()
        {
            _onPlayAgainClicked?.Invoke();
        }

        public void UpdateScore(int newScore)
        {
            _scoreCountable.SetNumber(newScore);
        }
        
        public void SwitchToInGameView()
        {
            _inGamePanel.SetActive(true);
            _beforeGamePanel.SetActive(false);
        }
        
        public void SwitchToBeforeGameView()
        {
            _inGamePanel.SetActive(false);
            _beforeGamePanel.SetActive(true);
        }

        public void UpdateTimePlaying(int timePlaying)
        {
            _timePlayingCountable.SetNumber(timePlaying);
        }  
        
        public void UpdateAsteroidsPassedCountable(int asteroidsPassed)
        {
            _asteroidsPassedCountable.SetNumber(asteroidsPassed);
        }

        public void UpdateHighScore(int highScore, bool isImmediate)
        {
            _highScoreCountable.SetNumber(highScore, isImmediate);
        }

        public void SetStartingValues(int highScore, int score, int timePlaying, int asteroidPassed)
        {
            _highScoreCountable.SetStartingValue(highScore);
            _asteroidsPassedCountable.SetStartingValue(asteroidPassed);
            _timePlayingCountable.SetStartingValue(timePlaying);
            _scoreCountable.SetStartingValue(score);
        }
    }
}