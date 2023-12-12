using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiViewModule
    {
        private MainGameUiView _mainGameUiView;

        public void SetupMainGameUiView(MainGameUiView mainGameUiView)
        {
            _mainGameUiView = mainGameUiView;
        }

        public void DestroyMainGameUiView()
        {
            Object.Destroy(_mainGameUiView.gameObject);
        }

        public void UpdateScore(int newScore)
        {
            _mainGameUiView.UpdateScore(newScore);
        }

        public void UpdateTimePlaying(int timePlaying)
        {
            _mainGameUiView.UpdateTimePlaying(timePlaying);
        }  
        
        public void UpdateAsteroidsPassedCountable(int asteroidsPassed)
        {
            _mainGameUiView.UpdateAsteroidsPassedCountable(asteroidsPassed);
        }

        public void UpdateHighScore(int lastHighScore, bool isImmediate)
        {
            _mainGameUiView.UpdateHighScore(lastHighScore, isImmediate);
        }
        
        public void SwitchToInGameView()
        {
            _mainGameUiView.SwitchToInGameView();
        }
        
        public void SwitchToBeforeGameView()
        {
            _mainGameUiView.SwitchToBeforeGameView();
        }

        public void SetStartingValues(int highScore, int score, int timePlaying, int asteroidPassed)
        {
            _mainGameUiView.SetStartingValues(highScore, score, timePlaying, asteroidPassed);
        }
        
        public void ShowGameOverPanel(int score,
            int timePlayed,
            int asteroidsPassed,
            bool isNewHighScore,
            int highScore)
        {
            _mainGameUiView.ShowGameOverPanel(score, timePlayed, asteroidsPassed, isNewHighScore, highScore);
        }
        
        public void HideGameOverPanel()
        {
            _mainGameUiView.HideGameOverPanel();
        }
    }
}
