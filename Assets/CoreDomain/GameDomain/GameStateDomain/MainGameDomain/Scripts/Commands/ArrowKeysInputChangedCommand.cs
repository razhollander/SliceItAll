using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.Scripts.Utils.Command;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands
{
    public class ArrowKeysInputChangedCommand : CommandSyncOneParameter<float, ArrowKeysInputChangedCommand>
    {
        private readonly float _directionValue;
        private readonly IPlayerSpaceshipModule _playerSpaceshipModule;

        public ArrowKeysInputChangedCommand(float directionValue, IPlayerSpaceshipModule playerSpaceshipModule)
        {
            _directionValue = directionValue;
            _playerSpaceshipModule = playerSpaceshipModule;
        }

        public override void Execute()
        {
            _playerSpaceshipModule.SetSpaceShipMoveDirection(_directionValue);
        }
    }
}
