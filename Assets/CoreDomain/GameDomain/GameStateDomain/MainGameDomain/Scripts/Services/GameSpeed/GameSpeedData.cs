using UnityEngine;

[CreateAssetMenu(fileName = "GameSpeedData", menuName = "Game/GameSpeed")]
public class GameSpeedData : ScriptableObject
{
    public float BaseSpeed = 5f;
    public float BoostSpeedMultiplier = 2f;
}