namespace CoreDomain.Services
{
    public interface IUpdateSubscriptionService
    {
        void RegisterUpdatable(IUpdatable updatable);
        void UnregisterUpdatable(IUpdatable updatable);
        void RegisterFixedUpdatable(IFixedUpdatable updatable);
        void UnregisterFixedUpdatable(IFixedUpdatable updatable);
    }
}