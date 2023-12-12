using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score
{
    [CreateAssetMenu(fileName = "GameScoreData", menuName = "Game/GameScore")]
    public class ScoreData : ScriptableObject
    {
        public int GainEverySecondScore = 1;
        public int BoostScoreMultiplier = 2;
    }
}