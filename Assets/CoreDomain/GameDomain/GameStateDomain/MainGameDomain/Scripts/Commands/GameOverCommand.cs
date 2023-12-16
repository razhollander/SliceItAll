using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services.GameStates;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class GameOverCommand : Command<GameOverCommand>
{
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly IStateMachineService _stateMachineService;
    private readonly MainGameState.Factory _mainGameStateFactory;
    private readonly IGameInputActionsModule _gameInputActionsModule;
    private readonly DisposeLevelCommand.Factory _disposeLevelCommand;
    private readonly StartLevelCommand.Factory _startLevelCommand;
    private readonly ILevelsService _levelsService;

    public GameOverCommand(IMainGameUiModule mainGameUiModule, IStateMachineService stateMachineService, MainGameState.Factory mainGameStateFactory,
        IGameInputActionsModule gameInputActionsModule, DisposeLevelCommand.Factory disposeLevelCommand, StartLevelCommand.Factory startLevelCommand, ILevelsService levelsService)
    {
        _mainGameUiModule = mainGameUiModule;
        _stateMachineService = stateMachineService;
        _mainGameStateFactory = mainGameStateFactory;
        _gameInputActionsModule = gameInputActionsModule;
        _disposeLevelCommand = disposeLevelCommand;
        _startLevelCommand = startLevelCommand;
        _levelsService = levelsService;
    }

    public override async UniTask Execute()
    {
        _mainGameUiModule.ShowGameOverPanel();
        _gameInputActionsModule.DisableInputs();

        await UniTaskHandler.WaitForAnyKeyPressed();

        _disposeLevelCommand.Create().Execute();
        _startLevelCommand.Create(new StartLevelCommandData(_levelsService.LastSavedLevelNumber)).Execute().Forget();

        //_stateMachineService.SwitchState(_mainGameStateFactory.Create(new MainGameStateEnterData()));
    }
}
