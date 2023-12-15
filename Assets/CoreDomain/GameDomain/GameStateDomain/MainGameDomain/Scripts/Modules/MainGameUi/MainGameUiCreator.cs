using CoreDomain.Services;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiCreator
    {
        private readonly IResourcesLoaderService _resourcesLoaderService;
        private const string MainGameUiAssetPath = @"UI\MainGameUiCanvas";

        public MainGameUiCreator(IResourcesLoaderService resourcesLoaderService)
        {
            _resourcesLoaderService = resourcesLoaderService;
        }

        public MainGameUiView CreateMainGameUi()
        {
            return _resourcesLoaderService.LoadAndInstantiate<MainGameUiView>(MainGameUiAssetPath);
        }
    }
}