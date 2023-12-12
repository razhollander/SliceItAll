using UnityEngine;

public interface IAsteroidsModule
{
    public int AsteroidsPassedPlayerCounter { get; }
    int AsteroidScoreGainedWhenPassedPlayer { get; }
    void LoadData();
    void SpawnAsteroid(Vector3 spawnPosition, float xPositionRange);
    void StartSpawning();
    void StopSpawning();
    void ResetTimeForNextSpawn();
    void SetAsteroidsPassedZPosition(float zPosition);
    void SetAsteroidPassedPlayer(string asteroidId);
    void Reset();
}