using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiModule : IMainGameUiModule
    {
        private readonly MainGameUiCreator _creator;
        private MainGameUiView _mainGameView;

        public MainGameUiModule(IResourcesLoaderService resourcesLoaderService)
        {
            _creator = new MainGameUiCreator(resourcesLoaderService);
        }

        public void UpdateScore(int newScore)
        {
            _mainGameView.UpdateScore(newScore);
        }
        
        public void ShowWinPanel(int winScore)
        {
            _mainGameView.ShowWinPanel(winScore);
        }

        public void CreateMainGameUi()
        {
            _mainGameView = _creator.CreateMainGameUi();
        }

        public void ShowGameOverPanel()
        {
            _mainGameView.ShowGameOverPanel();
        }
        
        public void HideGameOverPanel()
        {
            _mainGameView.HideGameOverPanel();
        }

        public void Dispose()
        {
            Object.Destroy(_mainGameView.gameObject);
        }
        
        public void SwitchToInGameView(int score)
        {
            _mainGameView.SwitchToInGameView();
            _mainGameView.SetStartingValues(score);
        }
        
        public void SwitchToBeforeGameView(int currentLevel)
        {
            _mainGameView.SwitchToBeforeGameView(currentLevel);
        }
    }
}