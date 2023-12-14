using UnityEngine;

public interface IArrowModule
{
    void FindArrow();
    Transform ArrowTransform { get; }
}