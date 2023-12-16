using UnityEngine;

public interface IArrowModule
{
    void SetupArrow();
    Transform ArrowTransform { get; }
    bool TryStabContactPoint(ContactPoint collisionContact);
    void Jump();
    void TryShoot();
}