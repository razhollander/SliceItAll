using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ArrowCollisionEnterCommand : CommandSyncOneParameter<ArrowCollisionEnterCommandData, ArrowCollisionEnterCommand>
{
    private readonly IArrowModule _arrowModule;
    private readonly IAudioService _audioService;
    private readonly GameOverCommand.Factory _gameOverCommand;
    private readonly Collision _collision;

    public ArrowCollisionEnterCommand(ArrowCollisionEnterCommandData commandData, IArrowModule arrowModule, IAudioService audioService, GameOverCommand.Factory gameOverCommand)
    {
        _arrowModule = arrowModule;
        _audioService = audioService;
        _gameOverCommand = gameOverCommand;
        _collision = commandData.Collision;
    }
    
    public override void Execute()
    {
        var isCollisionPopable = _collision.transform.GetComponent<PopableView>() != null;
        var didStab = false;
        
        if (!isCollisionPopable)
        {
            if (_collision.contacts.Length > 0)
            {
                didStab = _arrowModule.TryStabContactPoint(_collision.contacts[0]);
            }
            
            var isCollisionWithLava = _collision.transform.GetComponent<LavaView>() != null;
        
            if (isCollisionWithLava)
            {
                _gameOverCommand.Create().Execute().Forget();
            }
        }

        if (!didStab && !isCollisionPopable)
        {
            _audioService.PlayAudio(AudioClipName.Hit, AudioChannelType.Fx, AudioPlayType.OneShot);
        }
    }
}
