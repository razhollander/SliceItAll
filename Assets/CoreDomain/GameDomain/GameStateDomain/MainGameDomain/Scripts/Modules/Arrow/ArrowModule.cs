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
    private readonly ArrowParticleCollisionEnterCommand.Factory _arrowParticleCollisionEnterCommand;
    private readonly IAudioService _audioService;
    private readonly IResourcesLoaderService _resourcesLoaderService;
    public Transform ArrowTransform => _arrowView.transform;

    private ArrowView _arrowView;
    private bool _canStabInCurrentLoop = true;
    private bool _isCurrentlyStabbing;
    private TweenerCore<float,float,FloatOptions> _spinBeforeShootTween;
    private Vector3 _shootVector;
    private Vector3 _jumpVector;
    private float _prevZRotation;
    private ArrowMovementData _arrowMovementData;
    private ArrowCreator _arrowCreator;
    
    public ArrowModule(IUpdateSubscriptionService updateSubscriptionService, ArrowCollisionEnterCommand.Factory arrowCollisionEnterCommand,
        ArrowTriggerEnterCommand.Factory arrowTriggerEnterCommand,
        ArrowParticleCollisionEnterCommand.Factory arrowParticleCollisionEnterCommand,
        IAudioService audioService, IResourcesLoaderService resourcesLoaderService)
    {
        _arrowCreator = new ArrowCreator(resourcesLoaderService); 
        _updateSubscriptionService = updateSubscriptionService;
        _arrowCollisionEnterCommand = arrowCollisionEnterCommand;
        _arrowTriggerEnterCommand = arrowTriggerEnterCommand;
        _arrowParticleCollisionEnterCommand = arrowParticleCollisionEnterCommand;
        _audioService = audioService;
    }

    public void LoadArrowMovementData()
    {
        _arrowMovementData = _arrowCreator.LoadArrowMovementData();
        _shootVector = Quaternion.Euler(0, 0, _arrowMovementData.ShootAngleRelativeToFloor) * Vector3.right * _arrowMovementData.ShootVelocity;
        _jumpVector = Quaternion.Euler(0, 0, _arrowMovementData.JumpAngleRelativeToFloor) * Vector3.right * _arrowMovementData.JumpForce;
    }

    public void RegisterListeners()
    {
        _updateSubscriptionService.RegisterFixedUpdatable(this);
    }

    public void EnableThruster(bool isEnabled)
    {
        _arrowView.EnableThruster(isEnabled);
    }

    public void CreateArrow()
    {
        _arrowView = _arrowCreator.CreateArrow();
        _arrowView.transform.position = new Vector3(0, 10, 0);
        _arrowView.transform.rotation = Quaternion.Euler(0, 0, -50);  
        _arrowView.Setup(OnArrowCollisionEnter, OnArrowTriggerEnter, OnArrowParticleCollisionEnter, _arrowMovementData.AngularDrag);
    }

    public void Dispose()
    {
        _updateSubscriptionService.UnregisterFixedUpdatable(this);
        Object.Destroy(_arrowView.gameObject);
        _canStabInCurrentLoop = true;
        _isCurrentlyStabbing = false;
    }

    private async UniTask Shoot()
    {
        _audioService.PlayAudio(AudioClipName.Spin, AudioChannelType.Fx, AudioPlayType.OneShot);
        _arrowView.FreezeMovement(true, false);
        
        var currentRotationAngle = MathHandler.ConvertAngleToBeBetween0To360(_arrowView.GetZRotation());
        var loopAnimationAngles = currentRotationAngle;

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

        _spinBeforeShootTween.OnComplete(() =>
        {
            _arrowView.EnableThruster(true);
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
        _audioService.PlayAudio(AudioClipName.Jump, AudioChannelType.Fx, AudioPlayType.OneShot);

        _spinBeforeShootTween?.Kill();
        _isCurrentlyStabbing = false;
        
        EnableThruster(false);
        _arrowView.SetIsKinematic(false);
        _arrowView.SetGravity(true);
        _arrowView.SetVelocity(_jumpVector);
        _arrowView.SetZAngularVelocity(_arrowMovementData.SpacePressedRotationLoopSpeed);
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
        _arrowView.SetZAngularVelocity(_arrowMovementData.StartRotationLoopSpeed);
    }

    public bool TryStabContactPoint(ContactPoint collisionContact)
    {
        if (!_canStabInCurrentLoop || !DidStabContactPoint(collisionContact))
        {
            return false;
        }

        _audioService.PlayAudio(AudioClipName.Stab, AudioChannelType.Fx, AudioPlayType.OneShot);
        _arrowView.FreezeMovement(false, true);
        _isCurrentlyStabbing = true;
        _canStabInCurrentLoop = false;

        return true;
    }
    
    private void OnArrowCollisionEnter(Collision collision)
    {
        _arrowCollisionEnterCommand.Create(new ArrowCollisionEnterCommandData(collision)).Execute();
    }
    
    private void OnArrowTriggerEnter(Collider collider)
    {
        _arrowTriggerEnterCommand.Create(new ArrowTriggerEnterCommandData(collider)).Execute();
    }
    
    private void OnArrowParticleCollisionEnter(ParticleSystem particleSystem)
    {
        _arrowParticleCollisionEnterCommand.Create(new ArrowParticleCollisionEnterCommandData(particleSystem, _arrowView.gameObject)).Execute();
    }
    
    private bool DidStabContactPoint(ContactPoint contactPoint)
    {
        Vector3 contactPointNormal = contactPoint.normal;
        Vector3 hitVector = -_arrowView.transform.right;
        return Vector3.Angle(hitVector, contactPointNormal) < _arrowMovementData.MaxStabAngleWithSurface;
    }
}