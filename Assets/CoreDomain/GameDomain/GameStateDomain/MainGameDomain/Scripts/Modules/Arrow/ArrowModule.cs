using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class ArrowModule : IArrowModule, IFixedUpdatable
{
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private readonly ArrowCollisionEnterCommand.Factory _arrowCollisionEnterCommand;
    private readonly ArrowTriggerEnterCommand.Factory _arrowTriggerEnterCommand;
    public Transform ArrowTransform => _arrowView.transform;

    private ArrowView _arrowView;
    
    private float _spacePressedRotationLoopSpeed = -13.5f;
    private float _maxStabAngleWithSurface = 60;
    private float _startRotationLoopSpeed = -12;
    private float _shootAngleRelativeToFloor = -60;
    private float _shootRotationDuration = 0.5f;
    private float _jumpAngleRelativeToFloor = 75;
    private float _jumpForce=7;
    private float _shootVelocity = 40f;
    private float _angularDrag = 2;

    private bool _canStabInCurrentLoop;
    private bool _isCurrentlyStabbing;
    private TweenerCore<float,float,FloatOptions> _spinBeforeShootTween;
    private readonly Vector3 _shootVector;
    private readonly Vector3 _jumpVector;
    private float _prevZRotation;

    public ArrowModule(IUpdateSubscriptionService updateSubscriptionService, ArrowCollisionEnterCommand.Factory arrowCollisionEnterCommand, ArrowTriggerEnterCommand.Factory arrowTriggerEnterCommand)
    {
        _updateSubscriptionService = updateSubscriptionService;
        _arrowCollisionEnterCommand = arrowCollisionEnterCommand;
        _arrowTriggerEnterCommand = arrowTriggerEnterCommand;
        _shootVector = Quaternion.Euler(0, 0, _shootAngleRelativeToFloor) * Vector3.right * _shootVelocity;
        _jumpVector = Quaternion.Euler(0, 0, _jumpAngleRelativeToFloor) * Vector3.right * _jumpForce;
    }
    
    public void SetupArrow()
    {
        _arrowView = GameObject.FindObjectOfType<ArrowView>();

        if (_arrowView == null)
        {
            LogService.LogError("No Arrow Found In Scene!");
            return;
        }
        
        _arrowView.Setup(OnArrowCollisionEnter, OnArrowTriggerEnter, _angularDrag);
        _updateSubscriptionService.RegisterFixedUpdatable(this);
    }

    ~ArrowModule()
    {
        _updateSubscriptionService.UnregisterFixedUpdatable(this);
    }

    private async UniTask Shoot()
    {
        _arrowView.FreezeMovement(true, false);
        
        var currentRotationAngle = MathHandler.ConvertAngleToBeBetween0To360(_arrowView.GetZRotation());
        var loopAnimationAngles = currentRotationAngle;

        if (currentRotationAngle <180 && currentRotationAngle > _shootAngleRelativeToFloor)
        {
            loopAnimationAngles += 360;
        }
        
        _arrowView.SetZRotation(loopAnimationAngles);

        _spinBeforeShootTween?.Kill();
        _spinBeforeShootTween =  DOTween.To(
            () => loopAnimationAngles-_shootAngleRelativeToFloor,
            x =>
            {
                loopAnimationAngles = x + _shootAngleRelativeToFloor;
                _arrowView.SetZRotation(loopAnimationAngles);
            },
            0, _shootRotationDuration);

        _spinBeforeShootTween.OnComplete(() =>
        {
            Debug.Log("finishedTween");
            _arrowView.SetGravity(true);
            _arrowView.SetAngularVelocity(Vector3.zero);
            _arrowView.SetVelocity(_shootVector);
        });
        
        await _spinBeforeShootTween;
    
    }
    
    public void TryShoot()
    {
        if (_isCurrentlyStabbing) return;
        
        Shoot().Forget();
    }
    
    public void Jump()
    {
        Debug.Log("Jump!");
        _spinBeforeShootTween?.Kill();
        _isCurrentlyStabbing = false;

        _arrowView.SetIsKinematic(false);
        _arrowView.SetGravity(true);
        _arrowView.SetVelocity(_jumpVector);
        _arrowView.SetZAngularVelocity(_spacePressedRotationLoopSpeed);
    }

    public void ManagedFixedUpdate()
    {
        var currentZRotation = MathHandler.ConvertAngleToBeBetween0To360(_arrowView.GetZRotation());
        var shouldStartANewLoop = _prevZRotation is < 280 and > 270 && currentZRotation is > 260 and <= 270;
        
        if (shouldStartANewLoop)
        {
            SetLoopAngularVelocity();
        }

        if (!_canStabInCurrentLoop)
        {
            var minimalStabAngle = 85f;
            var didPassMinimalStabAngle = _prevZRotation < minimalStabAngle+30f && _prevZRotation > minimalStabAngle && currentZRotation > minimalStabAngle-30f && currentZRotation <= minimalStabAngle;

            if (didPassMinimalStabAngle)
            {
                _canStabInCurrentLoop = true;
            }
        }

        _prevZRotation = currentZRotation;
    }
    
    
    private void SetLoopAngularVelocity()
    {
        _arrowView.SetZAngularVelocity(_startRotationLoopSpeed);
    }

    public void TryStabContactPoint(ContactPoint collisionContact)
    {
        if (!_canStabInCurrentLoop || !DidStabContactPoint(collisionContact))
        {
            return;
        }

        _arrowView.FreezeMovement(false, true);
        _isCurrentlyStabbing = true;
        _canStabInCurrentLoop = false;
    }
    
    private void OnArrowCollisionEnter(Collision collision)
    {
        _arrowCollisionEnterCommand.Create(new ArrowCollisionEnterCommandData(collision)).Execute();
    }
    
    private void OnArrowTriggerEnter(Collider collider)
    {
        _arrowTriggerEnterCommand.Create(new ArrowTriggerEnterCommandData(collider)).Execute();
    }
    
    private bool DidStabContactPoint(ContactPoint contactPoint)
    {
        Vector3 contactPointNormal = contactPoint.normal;
        Vector3 hitVector = -_arrowView.transform.right;
        return Vector3.Angle(hitVector, contactPointNormal) < _maxStabAngleWithSurface;
    }
}