using CoreDomain.Services;
using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public class PlayerSpaceshipModule : IPlayerSpaceshipModule
    {
        public Transform PlayerSpaceShipTransform => _playerSpaceshipViewModule.PlayerSpaceShipTransform;
        
        private readonly PlayerHitCommand.Factory _playerHitCommand;
        private readonly PlayerSpaceshipCreator _playerSpaceshipCreator;
        private readonly PlayerSpaceshipViewModule _playerSpaceshipViewModule;
        private PlayerSpaceshipData _playerSpaceshipData;

        public PlayerSpaceshipModule(IAssetBundleLoaderService assetBundleLoaderService, IUpdateSubscriptionService updateSubscriptionService, PlayerHitCommand.Factory playerHitCommand)
        {
            _playerHitCommand = playerHitCommand;
            _playerSpaceshipCreator = new PlayerSpaceshipCreator(assetBundleLoaderService);
            _playerSpaceshipViewModule = new PlayerSpaceshipViewModule(updateSubscriptionService);
        }

        public void CreatePlayerSpaceship()
        {
            _playerSpaceshipData = _playerSpaceshipCreator.LoadPlayerSpaceShipData();
            var playerSpaceshipView = _playerSpaceshipCreator.CreatePlayerSpaceship();
            playerSpaceshipView.SetupCallbacks(OnSpaceshipCollisionEnter);
            _playerSpaceshipViewModule.Setup(playerSpaceshipView);
        }
        
        public void EnableSpaceShipMovement(bool isEnabled)
        {
            if (isEnabled)
            {
                _playerSpaceshipViewModule.RegisterListeners();
            }
            else
            {
                _playerSpaceshipViewModule.TrySetMovementVelocity(0);
                _playerSpaceshipViewModule.UnregisterListeners();
            }

            _playerSpaceshipViewModule.EnableThruster(isEnabled);
        }

        public void EnableThrusterBoost(bool isEnabled)
        {
            _playerSpaceshipViewModule.EnableThrusterBoost(isEnabled);
        }

        public void SetXMovementBounds(float positiveXBound)
        {
            _playerSpaceshipViewModule.SetXMovementBounds(positiveXBound);
        }

        private void OnSpaceshipCollisionEnter(Collider collision)
        {
            var didCollideWithAsteroid = collision.gameObject.GetComponent<AsteroidView>() != null;
            
            if (didCollideWithAsteroid)
            {
                _playerHitCommand.Create().Execute();
            }
        }

        public void SetSpaceShipMoveDirection(float xDirection)
        {
            _playerSpaceshipViewModule.TrySetMovementVelocity(xDirection * _playerSpaceshipData.MovementSpeed);
        }

        public void ResetSpaceShip()
        {
            _playerSpaceshipViewModule.EnableThrusterBoost(false);
            _playerSpaceshipViewModule.ResetSpaceShipTransform();
        }
    }
}