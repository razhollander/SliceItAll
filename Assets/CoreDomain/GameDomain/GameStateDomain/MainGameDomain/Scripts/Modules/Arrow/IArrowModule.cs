using UnityEngine;

public interface IArrowModule
{
    void CreateArrow();
    Transform ArrowTransform { get; }
    bool TryStabContactPoint(ContactPoint collisionContact);
    void Jump();
    void TryShoot();
    void Dispose();
    void LoadArrowMovementData();
    void RegisterListeners();
}