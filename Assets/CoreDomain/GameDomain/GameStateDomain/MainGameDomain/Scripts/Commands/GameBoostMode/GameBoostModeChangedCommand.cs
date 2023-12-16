using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameBoostModeChangedCommand : CommandOneParameter<GameBoostModeChangedCommandData, GameBoostModeChangedCommand>
{
    private readonly GameBoostModeChangedCommandData _commandData;
    private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
    private readonly ICameraService _cameraService;
    private readonly IGameSpeedService _gameSpeedService;
    private readonly IFloorModule _floorModule;
    private readonly IScoreModule _scoreModule;

    public GameBoostModeChangedCommand(
        GameBoostModeChangedCommandData commandData,
        IPlayerSpaceshipModule playerSpaceshipModule,
        ICameraService cameraService,
        IGameSpeedService gameSpeedService,
        IFloorModule floorModule,
        IScoreModule scoreModule)
    {
        _commandData = commandData;
        _playerSpaceshipModule = playerSpaceshipModule;
        _cameraService = cameraService;
        _gameSpeedService = gameSpeedService;
        _floorModule = floorModule;
        _scoreModule = scoreModule;
    }
    
   
    public override async UniTask Execute()
    {
        _gameSpeedService.SetBoostMode(_commandData.IsBoostOn);
        _cameraService.SetCameraZoom(GameCameraType.World, !_commandData.IsBoostOn);
        _playerSpaceshipModule.EnableThrusterBoost(_commandData.IsBoostOn);
    }
}
