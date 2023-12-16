using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class ArrowTriggerEnterCommand : CommandSyncOneParameter<ArrowTriggerEnterCommandData, ArrowTriggerEnterCommand>
{
    private readonly IFXModule _fxModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;
    private readonly Collider _collider;

    public ArrowTriggerEnterCommand(ArrowTriggerEnterCommandData commandData, IFXModule fxModule, ScoreChangedCommand.Factory scoreChangedCommand)
    {
        _fxModule = fxModule;
        _scoreChangedCommand = scoreChangedCommand;
        _collider = commandData.Collider;
    }
    
    public override void Execute()
    {
        var otherPopableView = _collider.GetComponent<PopableView>();

        if (otherPopableView != null)
        {
            otherPopableView.Pop();
            _fxModule.ShowScoreGainedFx(otherPopableView.transform.position, 1);
            _scoreChangedCommand.Create(new ScoreChangedCommandData(1)).Execute();
        }
    }
}
