using CoreDomain.Utils.Pools;

public class FXCreator
{
    private readonly ScoreGainedFXPool _scoreGainedFXPool;
    
    public FXCreator(ScoreGainedFXPool.Factory scoreGainedFXPool)
    {
        _scoreGainedFXPool = scoreGainedFXPool.Create(new PoolData(10, 5));
        _scoreGainedFXPool.InitPool();
    }

    public ScoreGainedFXView CreateScoreGainedFX()
    {
        return _scoreGainedFXPool.Spawn();
    }
}
