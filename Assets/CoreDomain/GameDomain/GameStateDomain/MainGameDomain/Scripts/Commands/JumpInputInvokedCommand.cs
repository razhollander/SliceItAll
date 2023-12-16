using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Commands
{
    public class JumpInputInvokedCommand : CommandSync<JumpInputInvokedCommand>
    {
        private readonly IArrowModule _arrowModule;
        private readonly IAudioService _audioService;

        private JumpInputInvokedCommand(IArrowModule arrowModule, IAudioService audioService)
        {
            _arrowModule = arrowModule;
            _audioService = audioService;
        }

        public override void Execute()
        {
            _audioService.PlayAudio(AudioClipName.Jump, AudioChannelType.Fx, AudioPlayType.OneShot);
            _arrowModule.Jump();
        }
    }
}