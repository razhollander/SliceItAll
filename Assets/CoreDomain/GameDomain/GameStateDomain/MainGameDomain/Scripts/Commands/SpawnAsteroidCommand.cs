using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

public class SpawnAsteroidCommand : CommandSync<SpawnAsteroidCommand>
{
    private readonly IAsteroidsModule _asteroidsModule;
    private readonly IFloorModule _floorModule;

    public SpawnAsteroidCommand(IAsteroidsModule asteroidsModule, IFloorModule floorModule)
    {
        _asteroidsModule = asteroidsModule;
        _floorModule = floorModule;
    }
    public override void Execute()
    {
        _asteroidsModule.SpawnAsteroid(_floorModule.FloorStartPoint, _floorModule.FloorHalfWidth);
        _asteroidsModule.ResetTimeForNextSpawn();
    }
}
