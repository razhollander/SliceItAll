using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ArrowCollisionEnterCommand : CommandSyncOneParameter<ArrowCollisionEnterCommandData, ArrowCollisionEnterCommand>
{
    private readonly IArrowModule _arrowModule;
    private readonly IAudioService _audioService;
    private readonly GameOverCommand.Factory _gameOverCommand;
    private readonly StabbedBullseyeCommand.Factory _stabbedBullseyeCommand;
    private readonly Collision _collision;

    public ArrowCollisionEnterCommand(ArrowCollisionEnterCommandData commandData, IArrowModule arrowModule, IAudioService audioService, GameOverCommand.Factory gameOverCommand,
        StabbedBullseyeCommand.Factory stabbedBullseyeCommand)
    {
        _arrowModule = arrowModule;
        _audioService = audioService;
        _gameOverCommand = gameOverCommand;
        _stabbedBullseyeCommand = stabbedBullseyeCommand;
        _collision = commandData.Collision;
    }
    
    public override void Execute()
    {
        var isCollisionPopable = _collision.transform.GetComponent<PopableView>() != null;
        var didStab = false;
        Debug.Log("Collision Enter!");
        if (!isCollisionPopable)
        {
            if (_collision.contacts.Length > 0)
            {
                didStab = _arrowModule.TryStabContactPoint(_collision.contacts[0]);
                Debug.Log("TryStab success: "+didStab);
            }

            if (didStab)
            {
                var isCollisionWithBullseye = _collision.transform.GetComponent<BullseyeView>() != null;

                if (isCollisionWithBullseye)
                {
                    _stabbedBullseyeCommand.Create().Execute().Forget();
                }
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
