using CoreDomain.Scripts.Utils.Command;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands
{
    public class JumpInputInvokedCommand : CommandSync<JumpInputInvokedCommand>
    {
        private readonly IArrowModule _arrowModule;

        private JumpInputInvokedCommand(IArrowModule arrowModule)
        {
            _arrowModule = arrowModule;
        }

        public override void Execute()
        {
            _arrowModule.Jump();
        }
    }
}