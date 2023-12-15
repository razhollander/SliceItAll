using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.Services;
using UnityEngine;

public class LevelCreator
{
   public GameObject CreateLevelTrack(GameObject levelTrackPrefab)
   {
      return Object.Instantiate(levelTrackPrefab);
   }
}
