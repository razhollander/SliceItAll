using System.Collections;
using System.Collections.Generic;
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

    public GameOverCommand(IMainGameUiModule mainGameUiModule, IStateMachineService stateMachineService, MainGameState.Factory mainGameStateFactory, IGameInputActionsModule gameInputActionsModule)
    {
        _mainGameUiModule = mainGameUiModule;
        _stateMachineService = stateMachineService;
        _mainGameStateFactory = mainGameStateFactory;
        _gameInputActionsModule = gameInputActionsModule;
    }

    public override async UniTask Execute()
    {
        _mainGameUiModule.ShowGameOverPanel();
        _gameInputActionsModule.DisableInputs();

        await WaitForAnyKeyPressed();

        _stateMachineService.SwitchState(_mainGameStateFactory.Create(new MainGameStateEnterData()));
    }
    
    private static async UniTask WaitForAnyKeyPressed()
    {
        await Observable.EveryUpdate().Where(_ => Input.anyKeyDown).First().ToTask();
    }
}
