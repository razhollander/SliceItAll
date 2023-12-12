using CoreDomain.Services;

public class FloorCreator
{
    private const string FloorAssetName = "Floor";
    private const string FloorAssetBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/floor";
        
    private readonly IAssetBundleLoaderService _assetBundleLoaderService;

    public FloorCreator(IAssetBundleLoaderService assetBundleLoaderService)
    {
        _assetBundleLoaderService = assetBundleLoaderService;
    }

    public FloorView CreateFloor()
    {
        return _assetBundleLoaderService.InstantiateAssetFromBundle<FloorView>(FloorAssetBundlePath, FloorAssetName);
    }
}
