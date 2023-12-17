
public class BalloonsCreator
{
    private readonly IResourcesLoaderService _resourcesLoaderService;
    private const string BalloonsDataAssetPath = "Configuration/BalloonsData";

    public BalloonsCreator(IResourcesLoaderService resourcesLoaderService)
    {
        _resourcesLoaderService = resourcesLoaderService;
    }

    public BalloonsData LoadBalloonsData()
    {
        return _resourcesLoaderService.Load<BalloonsData>(BalloonsDataAssetPath);
    }
}
