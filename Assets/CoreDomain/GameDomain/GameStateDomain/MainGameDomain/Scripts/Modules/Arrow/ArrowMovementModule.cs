using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class ArrowMovementModule
{
    private ArrowView _arrowView;
    private ArrowMovementData _arrowMovementData;
    private Vector3 _shootVector;
    private Vector3 _jumpVector;
    private TweenerCore<float,float,FloatOptions> _spinBeforeShootTween;

    public void SetArrow(ArrowView arrowView)
    {
        _arrowView = arrowView;
        _arrowView.SetAngularDrag(_arrowMovementData.AngularDrag);
    }
    
    public void SetArrowMovementData(ArrowMovementData arrowMovementData)
    {
        _arrowMovementData = arrowMovementData;
        _shootVector = Quaternion.Euler(0, 0, _arrowMovementData.ShootAngleRelativeToFloor) * Vector3.right * _arrowMovementData.ShootVelocity;
        _jumpVector = Quaternion.Euler(0, 0, _arrowMovementData.JumpAngleRelativeToFloor) * Vector3.right * _arrowMovementData.JumpForce;
    }
    
    public async UniTask Shoot()
    {
        FreezeMovement(true, false);
        
        var currentRotationAngle = MathHandler.ConvertAngleToBeBetween0To360(_arrowView.GetZRotation());
        var loopAnimationAngles = currentRotationAngle;

        // if the loopAngle is  small, we will extend it to have a longer animation
        if (currentRotationAngle <180 && currentRotationAngle > _arrowMovementData.ShootAngleRelativeToFloor)
        {
            loopAnimationAngles += 360;
        }
        
        _arrowView.SetZRotation(loopAnimationAngles);

        _spinBeforeShootTween?.Kill();
        _spinBeforeShootTween =  DOTween.To(
            () => loopAnimationAngles-_arrowMovementData.ShootAngleRelativeToFloor,
            x =>
            {
                loopAnimationAngles = x + _arrowMovementData.ShootAngleRelativeToFloor;
                _arrowView.SetZRotation(loopAnimationAngles);
            },
            0, _arrowMovementData.ShootRotationDuration);

        _spinBeforeShootTween.OnComplete(LaunchArrow);
        
        await _spinBeforeShootTween;
    }

    private void LaunchArrow()
    {
        _arrowView.EnableThruster(true);
        _arrowView.SetGravity(true);
        _arrowView.SetAngularVelocity(Vector3.zero);
        _arrowView.SetVelocity(_shootVector);
    }

    public void Jump()
    {
        _spinBeforeShootTween?.Kill();
        
        _arrowView.SetIsKinematic(false);
        _arrowView.SetGravity(true);
        _arrowView.SetVelocity(_jumpVector);
        _arrowView.SetZAngularVelocity(_arrowMovementData.SpacePressedRotationLoopSpeed);
    }

    public bool DidStabContactPoint(ContactPoint contactPoint)
    {
        Vector3 contactPointNormal = contactPoint.normal;
        Vector3 hitVector = -_arrowView.transform.right;
        return Vector3.Angle(hitVector, contactPointNormal) < _arrowMovementData.MaxStabAngleWithSurface;
    }

    public void FreezeMovement(bool isDisableGravity, bool isEnableKinematic)
    {
        _arrowView.FreezeMovement(false, true);
    }

    public void SetLoopAngularVelocity()
    {
        _arrowView.SetZAngularVelocity(_arrowMovementData.StartRotationLoopSpeed);
    }
}
