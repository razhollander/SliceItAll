using CoreDomain.Scripts.Services.DataPersistence;
using CoreDomain.Services;

namespace CoreDomain.GameDomain
{
    public class LevelsService : ILevelsService
    {
        private const string LevelsAssetFullPath = @"Levels\Data\LevelsSettings";
        private const string LastSavedLevelNumberKey = "LastLevelNumber";

        public int LastSavedLevelNumber => _lastSavedLevelNumber;
        
        private readonly IResourcesLoaderService _resourcesLoaderService;
        private LevelsData _levelsData;
        private int _lastSavedLevelNumber;
        private int _currentLevelNumber;
        private readonly IDataPersistence _dataPersistence;

        public LevelsService(IResourcesLoaderService resourcesLoaderService, IDataPersistence dataPersistence)
        {
            _resourcesLoaderService = resourcesLoaderService;
            _dataPersistence = dataPersistence;
        }

        public void LoadLevelsData()
        {
            _levelsData = _resourcesLoaderService.Load<LevelsData>(LevelsAssetFullPath);
            _lastSavedLevelNumber = _dataPersistence.Load(LastSavedLevelNumberKey, 1);
        }
        
        public void SetLastSavedLevel(int levelNumber)
        {
            _dataPersistence.Save(LastSavedLevelNumberKey, levelNumber);
            _lastSavedLevelNumber = levelNumber;
        }

        public int GetLevelsAmount()
        {
            return _levelsData.LevelsByOrder.Length;
        }

        public LevelData GetLevelData(int levelNumber)
        {
            return _levelsData.LevelsByOrder[levelNumber - 1];
        }
    }
}