using CoreDomain.Utils.Pools;
using Zenject;

public class ScoreGainedFXPool : ResourcesPool<ScoreGainedFXView, ScoreGainedFXPool>
{
    public ScoreGainedFXPool(PoolData poolData, DiContainer diContainer, IResourcesLoaderService resourcesLoaderService) : base(poolData, diContainer, resourcesLoaderService)
    {
    }

    public override string AssetPath => @"FX\ScoreGainedFX";
    protected override string ParentGameObjectName => "ScoreGainedFXParent";
}
