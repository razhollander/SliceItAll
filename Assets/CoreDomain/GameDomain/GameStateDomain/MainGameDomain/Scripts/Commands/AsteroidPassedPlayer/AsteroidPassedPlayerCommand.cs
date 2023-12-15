using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class AsteroidPassedPlayerCommand : CommandSyncOneParameter<AsteroidPassedPlayerCommandData, AsteroidPassedPlayerCommand>
{
    private readonly IScoreModule _scoreModule;
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly AsteroidPassedPlayerCommandData _commandData;
    private readonly IAsteroidsModule _asteroidsModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;

    public AsteroidPassedPlayerCommand(AsteroidPassedPlayerCommandData commandData, IAsteroidsModule asteroidsModule,ScoreChangedCommand.Factory scoreChangedCommand, IMainGameUiModule mainGameUiModule)
    {
        _commandData = commandData;
        _asteroidsModule = asteroidsModule;
        _scoreChangedCommand = scoreChangedCommand;
        _mainGameUiModule = mainGameUiModule;
    }
    
    public override void Execute()
    {
        _asteroidsModule.SetAsteroidPassedPlayer(_commandData.AsteroidId);
        _scoreChangedCommand.Create(new ScoreChangedCommandData(_asteroidsModule.AsteroidScoreGainedWhenPassedPlayer)).Execute();
    }
}
