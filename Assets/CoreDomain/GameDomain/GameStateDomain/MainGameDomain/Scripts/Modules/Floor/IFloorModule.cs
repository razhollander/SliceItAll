using UnityEngine;

public interface IFloorModule
{
    float FloorHalfWidth { get; }
    Vector3 FloorStartPoint { get; }
    void CreateFloor();
    void StartMovement();
    void StopMovement();
}