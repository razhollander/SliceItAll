public class BubblesCreator
{
    private readonly IResourcesLoaderService _resourcesLoaderService;
    private const string BubblesDataAssetPath = "Configuration/BubblesData";

    public BubblesCreator(IResourcesLoaderService resourcesLoaderService)
    {
        _resourcesLoaderService = resourcesLoaderService;
    }

    public BubblesData LoadBubblesData()
    {
        return _resourcesLoaderService.Load<BubblesData>(BubblesDataAssetPath);
    }
}
