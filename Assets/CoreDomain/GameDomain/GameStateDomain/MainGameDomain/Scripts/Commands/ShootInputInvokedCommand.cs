using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands
{
    public class ShootInputInvokedCommand : CommandSync<ShootInputInvokedCommand>
    {
        private readonly IArrowModule _arrowModule;

        private ShootInputInvokedCommand(IArrowModule arrowModule)
        {
            _arrowModule = arrowModule;
        }

        public override void Execute()
        {
            _arrowModule.TryShoot();
        }
    }
}