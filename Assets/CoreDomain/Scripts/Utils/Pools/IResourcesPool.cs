namespace CoreDomain.Utils.Pools
{
    public interface IResourcesPool<T> : IPool<T> where T : IPoolable
    {
        string AssetPath { get; }
    }
}
