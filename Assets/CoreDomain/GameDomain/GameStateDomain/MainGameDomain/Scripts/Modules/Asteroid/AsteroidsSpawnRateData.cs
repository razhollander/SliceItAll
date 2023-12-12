using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidsSpawnRateData", menuName = "Game/AsteroidsSpawnRate")]
public class AsteroidsSpawnRateData : ScriptableObject
{
    public float InitialSpawnRateInSeconds = 5f;
    public float FinalSpawnRateInSeconds = 3f;
    public float ReachFinalSpawnRateAfterSeconds = 30;
}