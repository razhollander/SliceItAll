using UnityEngine;

public class LevelTrackModule : ILevelTrackModule
{
    private LevelCreator _levelCreator;
    private GameObject _currentLevelTrack;
    
    public LevelTrackModule()
    {
        _levelCreator = new LevelCreator();
    }
    
    public void CreateLevelTrack(GameObject levelTrackPrefab)
    {
        _currentLevelTrack = _levelCreator.CreateLevelTrack(levelTrackPrefab);
    }

    public void Dispose()
    {
        Object.Destroy(_currentLevelTrack);
    }
}