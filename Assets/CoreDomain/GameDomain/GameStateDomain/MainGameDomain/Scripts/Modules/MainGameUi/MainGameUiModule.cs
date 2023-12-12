using CoreDomain.Services;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiModule : IMainGameUiModule
    {
        private readonly BeginGameCommand.Factory _beginGameCommand;
        private readonly ResetGameCommand.Factory _resetGameCommand;
        private readonly MainGameUiCreator _creator;
        private readonly MainGameUiViewModule _viewModule;

        public MainGameUiModule(IAssetBundleLoaderService assetBundleLoaderService, BeginGameCommand.Factory beginGameCommand, ResetGameCommand.Factory resetGameCommand)
        {
            _beginGameCommand = beginGameCommand;
            _resetGameCommand = resetGameCommand;
            _creator = new MainGameUiCreator(assetBundleLoaderService);
            _viewModule = new MainGameUiViewModule();
        }

        public void UpdateScore(int newScore)
        {
            _viewModule.UpdateScore(newScore);
        }

        public void CreateMainGameUi()
        {
            var mainGameUiView = _creator.CreateMainGameUi();
            mainGameUiView.Setup(OnPlayAgainClicked);
            _viewModule.SetupMainGameUiView(mainGameUiView);
        }

        public void ShowGameOverPanel(int score,
            int timePlayed,
            int asteroidsPassed,
            bool isNewHighScore,
            int highScore)
        {
            _viewModule.ShowGameOverPanel(score, timePlayed, asteroidsPassed, isNewHighScore, highScore);
        }
        
        public void HideGameOverPanel()
        {
            _viewModule.HideGameOverPanel();
        }

        public void Dispose()
        {
            _viewModule.DestroyMainGameUiView();
        }

        public void UpdateTimePlaying(int timePlaying)
        {
            _viewModule.UpdateTimePlaying(timePlaying);
        }
        
        public void UpdateAsteroidsPassedCountable(int asteroidsPassed)
        {
            _viewModule.UpdateAsteroidsPassedCountable(asteroidsPassed);
        }

        public void UpdateHighScore(int highScore, bool isImmediate = false)
        {
            _viewModule.UpdateHighScore(highScore, isImmediate);
        }
        
        public void SwitchToInGameView(int highScore, int score, int timePlaying, int asteroidPassed)
        {
            _viewModule.SwitchToInGameView();
            _viewModule.SetStartingValues(highScore, score, timePlaying, asteroidPassed);
        }
        
        public void SwitchToBeforeGameView()
        {
            _viewModule.SwitchToBeforeGameView();
        }
        
        private void OnPlayAgainClicked()
        {
            _resetGameCommand.Create().Execute();
            _beginGameCommand.Create().Execute();
        }
    }
}