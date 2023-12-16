namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public interface IMainGameUiModule
    {
        void UpdateScore(int newScore);
        void CreateMainGameUi();
        void Dispose();
        void SwitchToInGameView(int score);
        void SwitchToBeforeGameView(int currentLevel);
        void ShowGameOverPanel();
        void HideGameOverPanel();
        void ShowWinPanel(int winScore);
    }
}