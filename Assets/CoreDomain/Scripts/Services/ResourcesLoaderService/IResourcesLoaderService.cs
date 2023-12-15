using UnityEngine;

public interface IResourcesLoaderService
{
    T Load<T>(string fullPath) where T : Object;
    T LoadAndInstantiate<T>(string fullPath) where T : Component;
    T Load<T>(string path, string assetName) where T : Object;
}