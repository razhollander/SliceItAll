using Cysharp.Threading.Tasks;
using UnityEngine;

public class FXModule : IFXModule
{
    private FXCreator _fxCreator;
    
    public FXModule(ScoreGainedFXPool.Factory scoreGainedFXPool)
    {
        _fxCreator = new FXCreator(scoreGainedFXPool);
    }

    public async UniTask ShowScoreGainedFx(Vector3 position, int scoreGained)
    {
        var scoreGainedFXView = _fxCreator.CreateScoreGainedFX();
        scoreGainedFXView.Setup(scoreGained, position);
        
        await scoreGainedFXView.DoShowAnimation();
        
        scoreGainedFXView.Despawn();
    }
}
