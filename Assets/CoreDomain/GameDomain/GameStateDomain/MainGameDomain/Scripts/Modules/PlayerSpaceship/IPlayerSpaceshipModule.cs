using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    public interface IPlayerSpaceshipModule
    {
        public Transform PlayerSpaceShipTransform { get; }
        void CreatePlayerSpaceship();
        void SetSpaceShipMoveDirection(float xDirection);
        void ResetSpaceShip();
        void EnableSpaceShipMovement(bool isEnabled);
        void SetXMovementBounds(float positiveXBound);
        void EnableThrusterBoost(bool isEnabled);
    }
}