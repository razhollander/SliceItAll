using CoreDomain.Services;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class AsteroidsModule: IUpdatable, IAsteroidsModule
{
    public int AsteroidScoreGainedWhenPassedPlayer => _asteroidData.ScoreGainedWhenPassedPlayer;
    public int AsteroidsPassedPlayerCounter { get; private set; }

    private readonly SpawnAsteroidCommand.Factory _spawnAsteroidCommand;
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private readonly IGameSpeedService _gameSpeedService;
    private AsteroidsCreator _asteroidsCreator;
    private AsteroidsSpawnRateData _asteroidsSpawnRateData;
    private AsteroidData _asteroidData;
    private float _secondsUntilNextSpawn;
    private float _secondsPassedSinceStartedSpawning;
    private AsteroidsViewModule _asteroidsViewModule;

    public AsteroidsModule(
        AsteroidsPool.Factory asteroidsPool,
        IAssetBundleLoaderService assetBundleLoaderService,
        SpawnAsteroidCommand.Factory spawnAsteroidCommand,
        IUpdateSubscriptionService updateSubscriptionService,
        IGameSpeedService gameSpeedService,
        AsteroidPassedPlayerCommand.Factory asteroidPassedPlayerCommand)
    {
        _spawnAsteroidCommand = spawnAsteroidCommand;
        _updateSubscriptionService = updateSubscriptionService;
        _gameSpeedService = gameSpeedService;
        _asteroidsCreator = new AsteroidsCreator(asteroidsPool, assetBundleLoaderService);
        _asteroidsViewModule = new AsteroidsViewModule(gameSpeedService, updateSubscriptionService, asteroidPassedPlayerCommand);
    }

    public void LoadData()
    {
        _asteroidsSpawnRateData = _asteroidsCreator.LoadAsteroidsSpawnRateData();
        _asteroidData = _asteroidsCreator.LoadAsteroidData();
    }

    public void StartSpawning()
    {
        AddListeners();
        _asteroidsViewModule.StartMovingAsteroids();
    }

    public void Reset()
    {
        _asteroidsViewModule.DespawnAllAsteroids();
        _secondsUntilNextSpawn = 0;
        _secondsPassedSinceStartedSpawning = 0;
        AsteroidsPassedPlayerCounter = 0;
    }

    public void StopSpawning()
    {
        RemoveListeners();
        _asteroidsViewModule.StopMovingAsteroids();
    }

    public void ResetTimeForNextSpawn()
    {
        var secondsUntilNextSpawn = math.remap(0, _asteroidsSpawnRateData.ReachFinalSpawnRateAfterSeconds,
            _asteroidsSpawnRateData.InitialSpawnRateInSeconds, _asteroidsSpawnRateData.FinalSpawnRateInSeconds,
            _secondsPassedSinceStartedSpawning);
        var maxSpawnRateInSeconds = _asteroidsSpawnRateData.FinalSpawnRateInSeconds;
        _secondsUntilNextSpawn = Mathf.Max(secondsUntilNextSpawn, maxSpawnRateInSeconds);
    }

    public void ManagedUpdate()
    {
        var boostMultiplier = _gameSpeedService.IsBoosting ? _gameSpeedService.BoostSpeedMultiplier : 1;
        var timePassedInFrame = Time.deltaTime * boostMultiplier;

        _secondsUntilNextSpawn -= timePassedInFrame;
        _secondsPassedSinceStartedSpawning += timePassedInFrame;
        
        if (_secondsUntilNextSpawn <= 0)
        {
            _spawnAsteroidCommand.Create().Execute();
        }
    }

    public void SpawnAsteroid(Vector3 spawnPosition, float xPositionRange)
    {
        var asteroid = _asteroidsCreator.CreateAsteroid();
        var xPosition = Random.Range(spawnPosition.x - xPositionRange, spawnPosition.x + xPositionRange);
        asteroid.transform.position = new Vector3(xPosition, spawnPosition.y + asteroid.RendererHeight * 0.5f, spawnPosition.z);
        asteroid.gameObject.SetActive(true);
        
        _asteroidsViewModule.AddAsteroid(asteroid);
    }

    public void SetAsteroidPassedPlayer(string asteroidId)
    {
        _asteroidsViewModule.SetAsteroidPassedPlayer(asteroidId);
        AsteroidsPassedPlayerCounter++;
    }

    public void SetAsteroidsPassedZPosition(float zPosition)
    {
        _asteroidsViewModule.SetAsteroidsPassedZPosition(zPosition);
    }
    
    private void AddListeners()
    {
        _updateSubscriptionService.RegisterUpdatable(this);
    }
    
    private void RemoveListeners()
    {
        _updateSubscriptionService.UnregisterUpdatable(this);
    }
}