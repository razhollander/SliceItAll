using CoreDomain.Services;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipCreator
    {
        private const string PlayerSpaceshipAssetName = "PlayerSpaceship";
        private const string PlayerSpaceshipDataAssetName = "PlayerSpaceShipData";
        private const string PlayerSpaceshipAssetBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/playerspaceship";
        private const string PlayerSpaceshipAssetDataBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/configuration/playerspaceship";
        
        private readonly IAssetBundleLoaderService _assetBundleLoaderService;

        public PlayerSpaceshipCreator(IAssetBundleLoaderService assetBundleLoaderService)
        {
            _assetBundleLoaderService = assetBundleLoaderService;
        }

        public PlayerSpaceshipView CreatePlayerSpaceship()
        {
            return _assetBundleLoaderService.InstantiateAssetFromBundle<PlayerSpaceshipView>(PlayerSpaceshipAssetBundlePath, PlayerSpaceshipAssetName);
        }

        public PlayerSpaceshipData LoadPlayerSpaceShipData()
        {
            return _assetBundleLoaderService.LoadScriptableObjectAssetFromBundle<PlayerSpaceshipData>(PlayerSpaceshipAssetDataBundlePath, PlayerSpaceshipDataAssetName);
        }
    }
}
