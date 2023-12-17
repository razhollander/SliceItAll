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
        private readonly IAudioService _audioService;
        private readonly StartLevelCommand.Factory _startLevelCommand;
        private readonly IArrowModule _arrowModule;
        private readonly ILevelsService _levelsService;
        private readonly IBalloonsModule _balloonsModule;
        private readonly IBubblesModule _bubblesModule;

        public EnterMainGameStateCommand(
            MainGameStateEnterData stateEnterData,
            IMainGameUiModule mainGameUiModule,
            IAudioService audioService,
            StartLevelCommand.Factory startLevelCommand,
            IArrowModule arrowModule,
            ILevelsService levelsService,
            IBalloonsModule balloonsModule,
            IBubblesModule bubblesModule)
        {
            _stateEnterData = stateEnterData;
            _mainGameUiModule = mainGameUiModule;
            _startLevelCommand = startLevelCommand;
            _arrowModule = arrowModule;
            _levelsService = levelsService;
            _balloonsModule = balloonsModule;
            _bubblesModule = bubblesModule;
            _audioService = audioService;
        }

        public override async UniTask Execute()
        {
            LoadData();
            CreateGameObjects();
            
            _audioService.PlayAudio(AudioClipName.MainGameBGMusic, AudioChannelType.Master, AudioPlayType.Loop);
            _startLevelCommand.Create(new StartLevelCommandData(_levelsService.LastSavedLevelNumber)).Execute().Forget();
        }

        private void CreateGameObjects()
        {
            _mainGameUiModule.CreateMainGameUi();
        }

        private void LoadData()
        {
            _levelsService.LoadLevelsData();
            _arrowModule.LoadArrowMovementData();
            _balloonsModule.LoadData();
            _bubblesModule.LoadData();
        }
    }
}