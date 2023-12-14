using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands
{
    public class ShootInputInvokedCommand : CommandSync<ShootInputInvokedCommand>
    {
        private readonly GameBoostModeChangedCommand.Factory _gameBoostModeChangedCommand;

        private ShootInputInvokedCommand(GameBoostModeChangedCommand.Factory gameBoostModeChangedCommand)
        {
            _gameBoostModeChangedCommand = gameBoostModeChangedCommand;
        }

        public override void Execute()
        {
            _gameBoostModeChangedCommand.Create(new GameBoostModeChangedCommandData(false)).Execute().Forget();
        }
    }
}