using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;

public class GameSpeedService : IGameSpeedService
{
    private const string GameSpeedAssetBundlePath = "coredomain/gamedomain/gamestatedomain/maingamedomain/configuration/gamespeed";
    private const string GameSpeedSettingsAssetName = "GameSpeedData";

    public bool IsBoosting { get; private set; }
    public float CurrentGameSpeed => IsBoosting ? _gameSpeedConfigData.BoostSpeedMultiplier * _currentGameBaseSpeed : _currentGameBaseSpeed;
    public float BoostSpeedMultiplier => _gameSpeedConfigData.BoostSpeedMultiplier;
    
    public void Reset()
    {
        SetBoostMode(false);
    }

    private float _currentGameBaseSpeed;
    private GameSpeedData _gameSpeedConfigData;
    private readonly IAssetBundleLoaderService _assetBundleLoaderService;

    public GameSpeedService(IAssetBundleLoaderService assetBundleLoaderService)
    {
        _assetBundleLoaderService = assetBundleLoaderService;
    }

    public void LoadGameSpeedData()
    {
        _gameSpeedConfigData = _assetBundleLoaderService.LoadScriptableObjectAssetFromBundle<GameSpeedData>(GameSpeedAssetBundlePath, GameSpeedSettingsAssetName);
        _currentGameBaseSpeed = _gameSpeedConfigData.BaseSpeed;
    }

    public void SetBoostMode(bool isOn)
    {
        IsBoosting = isOn;
    }
}