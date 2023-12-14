using UnityEngine;

public interface IArrowModule
{
    void SetupArrow();
    Transform ArrowTransform { get; }
    void TryStabContactPoint(ContactPoint collisionContact);
    void Jump();
    void TryShoot();
}