using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCreator
{
    private readonly IResourcesLoaderService _resourcesLoaderService;
    private const string ArrowMovementAssetPath = "Arrow/ArrowMovementData";
    private const string ArrowAssetPath = "Arrow/Arrow";

    public ArrowCreator(IResourcesLoaderService resourcesLoaderService)
    {
        _resourcesLoaderService = resourcesLoaderService;
    }

    public ArrowView CreateArrow()
    {
        return _resourcesLoaderService.LoadAndInstantiate<ArrowView>(ArrowAssetPath);
    }
    
    public ArrowMovementData LoadArrowMovementData()
    {
        return _resourcesLoaderService.Load<ArrowMovementData>(ArrowMovementAssetPath);
    }
}
