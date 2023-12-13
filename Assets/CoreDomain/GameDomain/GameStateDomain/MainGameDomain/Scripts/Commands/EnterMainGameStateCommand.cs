using System.Threading.Tasks;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using CoreDomain.Services.GameStates;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain
{
    public class EnterMainGameStateCommand : CommandOneParameter<MainGameStateEnterData, EnterMainGameStateCommand>
    {
        private readonly MainGameStateEnterData _stateEnterData;
        private readonly IMainGameUiModule _mainGameUiModule;
        private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
        private readonly IAudioService _audioService;
        private readonly IFloorModule _floorModule;
        private readonly IGameSpeedService _gameSpeedService;
        private readonly ICameraService _cameraService;
        private readonly IAsteroidsModule _asteroidsModule;
        private readonly IScoreModule _scoreModule;
        private readonly BeginGameCommand.Factory _beginGameCommand;

        public EnterMainGameStateCommand(
            MainGameStateEnterData stateEnterData,
            IMainGameUiModule mainGameUiModule,
            IPlayerSpaceshipModule playerSpaceshipModule,
            IAudioService audioService,
            IFloorModule floorModule,
            IGameSpeedService gameSpeedService,
            ICameraService cameraService,
            IAsteroidsModule asteroidsModule,
            IScoreModule scoreModule,
            BeginGameCommand.Factory beginGameCommand)
        {
            _stateEnterData = stateEnterData;
            _mainGameUiModule = mainGameUiModule;
            _playerSpaceshipModule = playerSpaceshipModule;
            _floorModule = floorModule;
            _gameSpeedService = gameSpeedService;
            _cameraService = cameraService;
            _asteroidsModule = asteroidsModule;
            _scoreModule = scoreModule;
            _beginGameCommand = beginGameCommand;
            _audioService = audioService;
        }

        public override async UniTask Execute()
        {
            //LoadData();
            //CreateGameObjects();
            //SetupModules();
            
            _audioService.PlayAudio(AudioClipName.ThemeSongName, AudioChannelType.Master, AudioPlayType.Loop);

            //await WaitForAnyKeyPressed();

            //_beginGameCommand.Create().Execute();
        }

        private void SetupModules()
        {
            _cameraService.SetCameraFollowTarget(GameCameraType.World, _playerSpaceshipModule.PlayerSpaceShipTransform);
            _cameraService.SetCameraZoom(GameCameraType.World, true);
            _asteroidsModule.SetAsteroidsPassedZPosition(_playerSpaceshipModule.PlayerSpaceShipTransform.position.z);
            _mainGameUiModule.SwitchToBeforeGameView();
            _playerSpaceshipModule.SetXMovementBounds(_floorModule.FloorHalfWidth);
        }

        private void CreateGameObjects()
        {
            _mainGameUiModule.CreateMainGameUi();
            _playerSpaceshipModule.CreatePlayerSpaceship();
            _floorModule.CreateFloor();
        }

        private void LoadData()
        {
            _gameSpeedService.LoadGameSpeedData();
            _asteroidsModule.LoadData();
            _scoreModule.LoadScoreConfig();
        }

        private static async Task WaitForAnyKeyPressed()
        {
            await Observable.EveryUpdate().Where(_ => Input.anyKeyDown).First().ToTask();
        }
    }
}