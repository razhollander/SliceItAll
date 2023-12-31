using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
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
            Container.BindFactory<JumpInputInvokedCommand, JumpInputInvokedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ShootInputInvokedCommand, ShootInputInvokedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PoolData, ScoreGainedFXPool, ScoreGainedFXPool.Factory>().AsSingle().NonLazy();
            Container.BindFactory<MainGameStateEnterData, EnterMainGameStateCommand, EnterMainGameStateCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ExitMainGameStateCommand, ExitMainGameStateCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ScoreChangedCommandData, ScoreChangedCommand, ScoreChangedCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<StartLevelCommandData, StartLevelCommand, StartLevelCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ArrowCollisionEnterCommandData, ArrowCollisionEnterCommand, ArrowCollisionEnterCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ArrowTriggerEnterCommandData, ArrowTriggerEnterCommand, ArrowTriggerEnterCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<ArrowParticleCollisionEnterCommandData, ArrowParticleCollisionEnterCommand, ArrowParticleCollisionEnterCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<GameOverCommand, GameOverCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<StabbedBullseyeCommand, StabbedBullseyeCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<DisposeLevelCommand, DisposeLevelCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PopBalloonCommandData, PopBalloonCommand, PopBalloonCommand.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PopBubbleCommandData, PopBubbleCommand, PopBubbleCommand.Factory>().AsSingle().NonLazy();
        }

        private void BindModules()
        {
            Container.BindInterfacesTo<ArrowModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MainGameUiModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelTrackModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FXModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BalloonsModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BubblesModule>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<GameInputActionsModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelsService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreService>().AsSingle().NonLazy();
        }
    }
}