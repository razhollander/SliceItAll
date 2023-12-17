using UnityEngine;

public class LevelCreator
{
   public GameObject CreateLevelTrack(GameObject levelTrackPrefab)
   {
      return Object.Instantiate(levelTrackPrefab);
   }
}
