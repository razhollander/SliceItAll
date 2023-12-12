using UnityEngine;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship
{
    [CreateAssetMenu(fileName = "PlaceSpaceShipData", menuName = "Game/PlaceSpaceShip")]
    public class PlayerSpaceshipData : ScriptableObject
    {
        public float MovementSpeed = 5f;
    }
}
