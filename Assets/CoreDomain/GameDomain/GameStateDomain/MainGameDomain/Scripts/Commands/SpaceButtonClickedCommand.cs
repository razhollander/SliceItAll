using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands
{
    public class SpaceButtonClickedCommand : CommandSync<SpaceButtonClickedCommand>
    {
        private readonly GameBoostModeChangedCommand.Factory _gameBoostModeChangedCommand;

        private SpaceButtonClickedCommand(GameBoostModeChangedCommand.Factory gameBoostModeChangedCommand)
        {
            _gameBoostModeChangedCommand = gameBoostModeChangedCommand;
        }

        public override void Execute()
        {
            _gameBoostModeChangedCommand.Create(new GameBoostModeChangedCommandData(true)).Execute().Forget();
        }
    }
}