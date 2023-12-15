using System.Threading.Tasks;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
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
        private readonly StartLevelCommand.Factory _startLevelCommand;
        private readonly IArrowModule _arrowModule;
        private readonly IGameInputActionsModule _gameInputActionsModule;
        private readonly ILevelsService _levelsService;
        private readonly ILevelTrackModule _levelTrackModule;

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
            StartLevelCommand.Factory startLevelCommand,
            IArrowModule arrowModule,
            IGameInputActionsModule gameInputActionsModule,
            ILevelsService levelsService,
            ILevelTrackModule levelTrackModule)
        {
            _stateEnterData = stateEnterData;
            _mainGameUiModule = mainGameUiModule;
            _playerSpaceshipModule = playerSpaceshipModule;
            _floorModule = floorModule;
            _gameSpeedService = gameSpeedService;
            _cameraService = cameraService;
            _asteroidsModule = asteroidsModule;
            _scoreModule = scoreModule;
            _startLevelCommand = startLevelCommand;
            _arrowModule = arrowModule;
            _gameInputActionsModule = gameInputActionsModule;
            _levelsService = levelsService;
            _levelTrackModule = levelTrackModule;
            _audioService = audioService;
        }

        public override async UniTask Execute()
        {
            LoadData();
            CreateGameObjects();
            SetupModules();
            
            _audioService.PlayAudio(AudioClipName.ThemeSongName, AudioChannelType.Master, AudioPlayType.Loop);
            _startLevelCommand.Create(new StartLevelCommandData(_levelsService.LastSavedLevelNumber)).Execute().Forget();
        }
        
        private void SetupModules()
        {
            _arrowModule.SetupArrow();
            _cameraService.SetCameraFollowTarget(GameCameraType.World, _arrowModule.ArrowTransform);
            _gameInputActionsModule.EnableInputs();
        }

        private void CreateGameObjects()
        {
            _mainGameUiModule.CreateMainGameUi();
        }

        private void LoadData()
        {
            _levelsService.LoadLevelsData();
            _scoreModule.LoadScoreConfig();
        }
    }
}