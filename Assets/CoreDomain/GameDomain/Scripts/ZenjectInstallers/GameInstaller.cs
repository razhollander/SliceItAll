using CoreDomain.Services.GameStates;
using Zenject;

namespace CoreDomain.GameDomain
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<MainGameStateEnterData, MainGameState, MainGameState.Factory>();
        }
    }
}