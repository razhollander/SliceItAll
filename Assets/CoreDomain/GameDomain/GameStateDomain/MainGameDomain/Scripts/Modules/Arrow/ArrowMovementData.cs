using UnityEngine;

[CreateAssetMenu(fileName = "ArrowMovementData", menuName = "Game/ArrowMovementData")]
public class ArrowMovementData : ScriptableObject
{
    public float SpacePressedRotationLoopSpeed = -13.5f;
    public float MaxStabAngleWithSurface = 60;
    public float StartRotationLoopSpeed = -12;
    public float ShootAngleRelativeToFloor = -60;
    public float ShootRotationDuration = 0.5f;
    public float JumpAngleRelativeToFloor = 75;
    public float JumpForce= 8;
    public float ShootVelocity = 40f;
    public float AngularDrag = 2;
}