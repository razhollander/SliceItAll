using CoreDomain.Scripts.Extensions;
using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipViewModule: IUpdatable
    {
        private const float SpaceShipYPosition = 3.8f;
        private const float SpaceshipMaxRotationAngle = 20;
        
        public Transform PlayerSpaceShipTransform => _playerSpaceshipView.transform;

        private PlayerSpaceshipView _playerSpaceshipView;
        private float _playerSpaceFromBounds;
        private float _spaceshipDestRotation;
        private readonly IUpdateSubscriptionService _updateSubscriptionService;
        private float _positiveXMovementBound;
        
        public PlayerSpaceshipViewModule(IUpdateSubscriptionService updateSubscriptionService)
        {
            _updateSubscriptionService = updateSubscriptionService;
        }
        
        public void Setup(PlayerSpaceshipView playerSpaceshipView)
        {
            _playerSpaceshipView = playerSpaceshipView;
            ResetSpaceShipTransform();
        }

        public void RegisterListeners()
        {
            _updateSubscriptionService.RegisterUpdatable(this);
        }
        
        public void UnregisterListeners()
        {
            _updateSubscriptionService.UnregisterUpdatable(this);
        }
        
        public void TrySetMovementVelocity(float xVelocity)
        {
            var spaceShipPosition = _playerSpaceshipView.transform.position;
            var willMoveOutOfBounds = spaceShipPosition.x > _positiveXMovementBound && xVelocity > 0 ||
                                      spaceShipPosition.x < -_positiveXMovementBound && xVelocity < 0;
            
            if (willMoveOutOfBounds) return;
            
            SetMovementVelocity(xVelocity);
        }

        public void EnableThrusterBoost(bool isEnabled)
        {
            _playerSpaceshipView.EnableThrusterBoost(isEnabled);
        }
        
        public void ManagedUpdate()
        {
            _playerSpaceshipView.LerpToRotation(_spaceshipDestRotation);
            ResetVelocityIfOutOfBounds();
        }

        public void ResetSpaceShipTransform()
        {
            _playerSpaceshipView.transform.position = new Vector3(0, SpaceShipYPosition, 0);
            _playerSpaceshipView.SetRotation(0);
        }

        public void SetXMovementBounds(float positiveXBound)
        {
            _positiveXMovementBound = positiveXBound;
        }

        public void EnableThruster(bool isEnabled)
        {
            _playerSpaceshipView.EnableThruster(isEnabled);
        }
        
        private void ResetVelocityIfOutOfBounds()
        {
            var spaceShipPosition = _playerSpaceshipView.transform.position;
            var isSpaceShipOutOfXBounds =  spaceShipPosition.x > _positiveXMovementBound ||
                                           spaceShipPosition.x < -_positiveXMovementBound;
            
            if (isSpaceShipOutOfXBounds)
            {
                SetMovementVelocity(0);
            }
        }
        
        private void SetMovementVelocity(float xVelocity)
        {
            _playerSpaceshipView.SetVelocity(xVelocity);
            var xDirection = xVelocity.GetSign();
            var rotationFactor = -xDirection;
            _spaceshipDestRotation = SpaceshipMaxRotationAngle * rotationFactor;
        }
    }
}