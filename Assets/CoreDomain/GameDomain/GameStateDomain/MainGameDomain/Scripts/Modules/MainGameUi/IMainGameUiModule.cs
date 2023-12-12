namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public interface IMainGameUiModule
    {
        void UpdateScore(int newScore);
        void CreateMainGameUi();
        void Dispose();
        void UpdateTimePlaying(int timePlaying);
        void UpdateAsteroidsPassedCountable(int asteroidsPassed);
        void UpdateHighScore(int highScore, bool isImmediate);
        void SwitchToInGameView(int highScore, int score, int timePlaying, int asteroidPassed);
        void SwitchToBeforeGameView();

        void ShowGameOverPanel(
            int score,
            int timePlayed,
            int asteroidsPassed,
            bool isNewHighScore,
            int highScore);

        void HideGameOverPanel();
    }
}