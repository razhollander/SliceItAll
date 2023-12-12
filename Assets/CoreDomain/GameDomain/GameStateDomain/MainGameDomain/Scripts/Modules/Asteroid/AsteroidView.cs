using System;
using CoreDomain.Utils.Pools;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IPoolable
{
    [SerializeField] private Renderer _renderer;
    public float RendererHeight => _renderer.bounds.size.y;
    public string ID;
    public Action Despawn { get; set; }
    
    public void OnSpawned()
    {
        ID = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }

    public void OnDespawned()
    {
        gameObject.SetActive(false);
    }
}
