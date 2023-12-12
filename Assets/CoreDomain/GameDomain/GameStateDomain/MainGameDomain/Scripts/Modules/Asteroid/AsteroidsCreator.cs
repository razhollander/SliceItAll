using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using UnityEngine;
using CoreDomain.Utils.Pools;

public class AsteroidsCreator
{
    private const string AsteroidsSpawnRateAssetBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/configuration/asteroidsspawnrate";
    private const string AsteroidsSpawnRateAssetName = "AsteroidsSpawnRateData";

    private const string AsteroidAssetBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/configuration/asteroid";
    private const string AsteroidAssetName = "AsteroidData";
    
    private AsteroidsPool _asteroidsPool;
    private readonly IAssetBundleLoaderService _assetBundleLoaderService;

    public AsteroidsCreator(AsteroidsPool.Factory asteroidsPool, IAssetBundleLoaderService assetBundleLoaderService)
    {
        _assetBundleLoaderService = assetBundleLoaderService;
        _asteroidsPool = asteroidsPool.Create(new PoolData(10, 5));
        _asteroidsPool.InitPool();
    }

    public AsteroidView CreateAsteroid()
    {
        return _asteroidsPool.Spawn();
    }
    
    public AsteroidsSpawnRateData LoadAsteroidsSpawnRateData()
    {
        return _assetBundleLoaderService.LoadScriptableObjectAssetFromBundle<AsteroidsSpawnRateData>(AsteroidsSpawnRateAssetBundlePath, AsteroidsSpawnRateAssetName);
    }
    
    public AsteroidData LoadAsteroidData()
    {
        return _assetBundleLoaderService.LoadScriptableObjectAssetFromBundle<AsteroidData>(AsteroidAssetBundlePath, AsteroidAssetName);
    }
}
