using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class ArrowModule : IArrowModule, IFixedUpdatable
{
    public Transform ArrowTransform => _arrowView.transform;

    private readonly Vector3 ArrowStartPos = new (0, 10, 0);
    private readonly Quaternion ArrowStartRot = Quaternion.Euler(0, 0, -50);  
    
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private readonly ArrowCollisionEnterCommand.Factory _arrowCollisionEnterCommand;
    private readonly ArrowTriggerEnterCommand.Factory _arrowTriggerEnterCommand;
    private readonly ArrowParticleCollisionEnterCommand.Factory _arrowParticleCollisionEnterCommand;
    private readonly IAudioService _audioService;
    private readonly IResourcesLoaderService _resourcesLoaderService;

    private ArrowView _arrowView;
    private bool _canStabInCurrentLoop = true;
    private bool _isCurrentlyStabbing;
    private float _prevZRotation;
    private ArrowCreator _arrowCreator;
    private ArrowMovementModule _arrowMovementModule;
    
    public ArrowModule(IUpdateSubscriptionService updateSubscriptionService, ArrowCollisionEnterCommand.Factory arrowCollisionEnterCommand,
        ArrowTriggerEnterCommand.Factory arrowTriggerEnterCommand,
        ArrowParticleCollisionEnterCommand.Factory arrowParticleCollisionEnterCommand,
        IAudioService audioService, IResourcesLoaderService resourcesLoaderService)
    {
        _arrowCreator = new ArrowCreator(resourcesLoaderService);
        _arrowMovementModule = new ArrowMovementModule();
        _updateSubscriptionService = updateSubscriptionService;
        _arrowCollisionEnterCommand = arrowCollisionEnterCommand;
        _arrowTriggerEnterCommand = arrowTriggerEnterCommand;
        _arrowParticleCollisionEnterCommand = arrowParticleCollisionEnterCommand;
        _audioService = audioService;
    }

    public void LoadArrowMovementData()
    {
        _arrowMovementModule.SetArrowMovementData(_arrowCreator.LoadArrowMovementData());
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
        var arrowTransform = _arrowView.transform;
        arrowTransform.position = ArrowStartPos;
        arrowTransform.rotation = ArrowStartRot; 
        _arrowView.SetupCallbacks(OnArrowCollisionEnter, OnArrowTriggerEnter, OnArrowParticleCollisionEnter);
        _arrowMovementModule.SetArrow(_arrowView);
    }

    public void Dispose()
    {
        _updateSubscriptionService.UnregisterFixedUpdatable(this);
        Object.Destroy(_arrowView.gameObject);
        _canStabInCurrentLoop = true;
        _isCurrentlyStabbing = false;
    }

    private void Shoot()
    {
        _audioService.PlayAudio(AudioClipName.Spin, AudioChannelType.Fx, AudioPlayType.OneShot);
        _arrowMovementModule.Shoot().Forget();
    }

    public void TryShoot()
    {
        if (_isCurrentlyStabbing) return;

        Shoot();
    }
    
    public void Jump()
    {
        _audioService.PlayAudio(AudioClipName.Jump, AudioChannelType.Fx, AudioPlayType.OneShot);
        _arrowMovementModule.Jump();
        EnableThruster(false);
        _isCurrentlyStabbing = false;
    }

    public void ManagedFixedUpdate()
    {
        var currentZRotation = MathHandler.ConvertAngleToBeBetween0To360(_arrowView.GetZRotation());

        TrySelfLoop(currentZRotation);
        TryResetIfCanStab(currentZRotation);
        
        _prevZRotation = currentZRotation;
    }

    public bool TryStabContactPoint(ContactPoint collisionContact)
    {
        if (!_canStabInCurrentLoop || !_arrowMovementModule.DidStabContactPoint(collisionContact))
        {
            return false;
        }

        _audioService.PlayAudio(AudioClipName.Stab, AudioChannelType.Fx, AudioPlayType.OneShot);
        _arrowMovementModule.FreezeMovement(false, true);
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

    private void TryResetIfCanStab(float currentZRotation)
    {
        if (!_canStabInCurrentLoop)
        {
            var minimalStabAngle = 85f;
            var didPassMinimalStabAngle = _prevZRotation < minimalStabAngle+30f && _prevZRotation > minimalStabAngle && currentZRotation > minimalStabAngle-30f && currentZRotation <= minimalStabAngle;

            if (didPassMinimalStabAngle)
            {
                _canStabInCurrentLoop = true;
            }
        }
    }

    private void TrySelfLoop(float currentZRotation)
    {
        var shouldStartANewLoop = _prevZRotation is < 280 and > 270 && currentZRotation is > 260 and <= 270;

        if (shouldStartANewLoop)
        {
            _arrowMovementModule.SetLoopAngularVelocity();
        }
    }
}