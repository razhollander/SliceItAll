using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidData", menuName = "Game/AsteroidData")]
public class AsteroidData : ScriptableObject
{
    public int ScoreGainedWhenPassedPlayer = 5;
}
