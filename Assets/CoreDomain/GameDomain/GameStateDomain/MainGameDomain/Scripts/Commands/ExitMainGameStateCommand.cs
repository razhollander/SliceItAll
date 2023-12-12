using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain
{
    public class ExitMainGameStateCommand : Command<ExitMainGameStateCommand>
    {
        public ExitMainGameStateCommand()
        {
        }

        public override async UniTask Execute()
        {
            
        }
    }
}