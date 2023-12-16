using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StabbedBullseyeCommand : Command<StabbedBullseyeCommand>
{
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly IScoreModule _scoreModule;
    private readonly IGameInputActionsModule _gameInputActionsModule;

    public StabbedBullseyeCommand(IMainGameUiModule mainGameUiModule, IScoreModule scoreModule, IGameInputActionsModule gameInputActionsModule)
    {
        _mainGameUiModule = mainGameUiModule;
        _scoreModule = scoreModule;
        _gameInputActionsModule = gameInputActionsModule;
    }

    public override async UniTask Execute()
    {
        _mainGameUiModule.ShowWinPanel(_scoreModule.PlayerScore);
        _gameInputActionsModule.DisableInputs();
        
        await UniTaskHandler.WaitForAnyKeyPressed();
        
    }
}
