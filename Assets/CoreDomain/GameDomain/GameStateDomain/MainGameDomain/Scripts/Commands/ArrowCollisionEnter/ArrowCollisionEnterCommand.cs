using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class ArrowCollisionEnterCommand : CommandSyncOneParameter<ArrowCollisionEnterCommandData, ArrowCollisionEnterCommand>
{
    private readonly IArrowModule _arrowModule;
    private readonly Collision _collision;

    public ArrowCollisionEnterCommand(ArrowCollisionEnterCommandData commandData, IArrowModule arrowModule)
    {
        _arrowModule = arrowModule;
        _collision = commandData.Collision;
    }
    
    public override void Execute()
    {
        var isCollisionPopable = _collision.transform.GetComponent<PopableView>() != null;

        if (isCollisionPopable)
        {
            // arrow white effect
        }
        else
        {
            if (_collision.contacts.Length > 0)
            {
                _arrowModule.TryStabContactPoint(_collision.contacts[0]);
            }
            
            var isCollisionWithLava = _collision.transform.GetComponent<LavaView>() != null;
        
            if (isCollisionWithLava)
            {
                // game over
            }
        }
    }
}
