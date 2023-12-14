using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class ArrowTriggerEnterCommand : CommandSyncOneParameter<ArrowTriggerEnterCommandData, ArrowTriggerEnterCommand>
{
    private readonly Collider _collider;

    public ArrowTriggerEnterCommand(ArrowTriggerEnterCommandData commandData)
    {
        _collider = commandData.Collider;
    }
    
    public override void Execute()
    {
        var otherPopableView = _collider.GetComponent<PopableView>();

        if (otherPopableView != null)
        {
            otherPopableView.Pop();
        }
    }
}
