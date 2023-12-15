using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesLoaderService : IResourcesLoaderService
{
    public T Load<T>(string fullPath) where T : Object
    {
        return Resources.Load<T>(fullPath);
    }

    public T LoadAndInstantiate<T>(string fullPath) where T : Component
    {
        return Object.Instantiate(Load<T>(fullPath).gameObject).GetComponent<T>();
    }

    public T Load<T>(string path, string assetName) where T : Object
    {
        return Load<T>(Path.Combine(path, assetName));
    }
}
