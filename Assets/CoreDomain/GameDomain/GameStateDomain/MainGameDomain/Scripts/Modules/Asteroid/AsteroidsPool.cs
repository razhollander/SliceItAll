using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using CoreDomain.Utils.Pools;
using UnityEngine;
using Zenject;

public class AsteroidsPool : AssetFromBundlePool<AsteroidView, AsteroidsPool>
{
    public AsteroidsPool(PoolData poolData, DiContainer diContainer, IAssetBundleLoaderService assetBundleLoaderService) : base(poolData, diContainer, assetBundleLoaderService)
    {
    }

    protected override string AssetBundlePathName => "coredomain/gamedomain/gamestatedomain/maingamedomain/asteroid";
    public override string AssetName => "Asteroid";
    protected override string ParentGameObjectName => "AsteroidsPool";
}
