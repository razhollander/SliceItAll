namespace CoreDomain.GameDomain
{
    public interface ILevelsService
    {
        int LastSavedLevelNumber { get; }
        void LoadLevelsData();
        int GetLevelsAmount();
        LevelData GetLevelData(int levelNumber);
        void SetLastSavedLevel(int levelNumber);
    }
}