using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CoreDomain.Utils.Pools
{
    public abstract class ResourcesPool<TPoolable, TV> : BasePool<TPoolable, TV>, IResourcesPool<TPoolable> where TPoolable : MonoBehaviour, IPoolable
    {
        private DiContainer _diContainer;
        private readonly IResourcesLoaderService _resourcesLoaderService;
        public abstract string AssetPath { get; }
        protected abstract string ParentGameObjectName { get; }
        private Transform _parentGameObject;

        public ResourcesPool(PoolData poolData, DiContainer diContainer, IResourcesLoaderService resourcesLoaderService) : base(poolData)
        {
            _diContainer = diContainer;
            _resourcesLoaderService = resourcesLoaderService;
        }

        public override void InitPool()
        {
            _parentGameObject = new GameObject(ParentGameObjectName).transform;
            base.InitPool();
        }

        protected override List<TPoolable> CreatePoolableInstances(int instancesAmount)
        {
            var poolables = new List<TPoolable>();
            var asset = _resourcesLoaderService.Load<TPoolable>(AssetPath);

            for (int i = 0; i < instancesAmount; i++)
            {
                var poolable = _diContainer.InstantiatePrefab(asset.gameObject);
                poolable.SetActive(false);
                poolable.transform.SetParent(_parentGameObject);
                poolables.Add(poolable.GetComponent<TPoolable>());
            }

            return poolables;
        }

        protected override void Despawn(TPoolable obj)
        {
            if (obj == null)
            {
                return;
            }

            obj.transform.SetParent(_parentGameObject);
            base.Despawn(obj);
        }
    }
}