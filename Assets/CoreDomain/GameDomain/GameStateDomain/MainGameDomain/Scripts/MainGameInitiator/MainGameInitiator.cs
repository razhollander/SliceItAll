using CoreDomain.GameDomain.GameStateDomain.LobbyDomain;
using CoreDomain.Services.GameStates;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain
{
    public class MainGameInitiator : MonoBehaviour, IStateInitiator<MainGameStateEnterData>
    {
        [SerializeField] private MainGameStateEnterData _defaultLobbyGameStateEnterData;

        private EnterMainGameStateCommand.Factory _enterMainGameStateCommandFactory;
        private ExitMainGameStateCommand.Factory _exitMainGameStateCommandFactory;

        [Inject]
        private void Setup(EnterMainGameStateCommand.Factory enterMainGameStateCommandFactory, ExitMainGameStateCommand.Factory exitMainGameStateCommandFactory)
        {
            _enterMainGameStateCommandFactory = enterMainGameStateCommandFactory;
            _exitMainGameStateCommandFactory = exitMainGameStateCommandFactory;
        }
        
        public async UniTask EnterState(MainGameStateEnterData stateEnterData = null)
        {
            var enterData = stateEnterData ?? _defaultLobbyGameStateEnterData;
            _enterMainGameStateCommandFactory.Create(enterData).Execute().Forget();
        }

        public async UniTask ExitState()
        {
            await _exitMainGameStateCommandFactory.Create().Execute();
        }
    }
}