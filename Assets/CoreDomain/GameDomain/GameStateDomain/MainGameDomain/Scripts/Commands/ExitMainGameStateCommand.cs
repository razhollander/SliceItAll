using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain
{
    public class ExitMainGameStateCommand : Command<ExitMainGameStateCommand>
    {
        private readonly IArrowModule _arrowModule;

        public ExitMainGameStateCommand(IArrowModule arrowModule)
        {
            _arrowModule = arrowModule;
        }

        public override async UniTask Execute()
        {
            _arrowModule.Dispose();
        }
    }
}