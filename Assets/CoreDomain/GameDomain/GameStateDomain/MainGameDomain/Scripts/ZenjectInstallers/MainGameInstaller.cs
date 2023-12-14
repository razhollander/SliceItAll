using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Services.GameStates;
using CoreDomain.Utils.Pools;
using Zenject;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain
{
    public class MainGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindModules();
            BindCommands();
        }

        private void BindCommands()
        {
            Container.BindFactory<GameBoostModeChangedCommandData, GameBoostModeChangedCommand, GameBoostModeChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<JumpInputInvokedCommand, JumpInputInvokedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ShootInputInvokedCommand, ShootInputInvokedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PoolData, AsteroidsPool, AsteroidsPool.Factory>().AsSingle().NonLazy();
            Container.BindFactory<MainGameStateEnterData, EnterMainGameStateCommand, EnterMainGameStateCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ExitMainGameStateCommand, ExitMainGameStateCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<SpawnAsteroidCommand, SpawnAsteroidCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<float, ArrowKeysInputChangedCommand, ArrowKeysInputChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ScoreChangedCommandData, ScoreChangedCommand, ScoreChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<int, TimePlayingChangedCommand, TimePlayingChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<BeginGameCommand, BeginGameCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PlayerHitCommand, PlayerHitCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ResetGameCommand, ResetGameCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<AsteroidPassedPlayerCommandData, AsteroidPassedPlayerCommand, AsteroidPassedPlayerCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ArrowCollisionEnterCommandData, ArrowCollisionEnterCommand, ArrowCollisionEnterCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ArrowTriggerEnterCommandData, ArrowTriggerEnterCommand, ArrowTriggerEnterCommand.Factory>().AsSingle().NonLazy();
        }

        private void BindModules()
        {
            Container.BindInterfacesTo<ArrowModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MainGameUiModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerSpaceshipModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FloorModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AsteroidsModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TimePlayingModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HighScoreModule>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<GameInputActionsModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameSpeedService>().AsSingle().NonLazy();
        }
    }
}