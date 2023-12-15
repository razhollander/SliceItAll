using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class ArrowTriggerEnterCommand : CommandSyncOneParameter<ArrowTriggerEnterCommandData, ArrowTriggerEnterCommand>
{
    private readonly IFXModule _fxModule;
    private readonly Collider _collider;

    public ArrowTriggerEnterCommand(ArrowTriggerEnterCommandData commandData, IFXModule fxModule)
    {
        _fxModule = fxModule;
        _collider = commandData.Collider;
    }
    
    public override void Execute()
    {
        var otherPopableView = _collider.GetComponent<PopableView>();

        if (otherPopableView != null)
        {
            otherPopableView.Pop();
            _fxModule.ShowScoreGainedFx(otherPopableView.transform.position, 1);
        }
    }
}
