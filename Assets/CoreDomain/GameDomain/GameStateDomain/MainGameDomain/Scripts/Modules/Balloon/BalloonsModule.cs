using System.Collections;
using System.Collections.Generic;
using CoreDomain.Services;
using UnityEngine;

public class BalloonsModule : IBalloonsModule
{
    private readonly PopBalloonCommand.Factory _balloonCommand;
    public int BalloonPopScore => _balloonsData.PopScore;
    
    private BalloonsCreator _balloonsCreator;
    private BalloonsData _balloonsData;
    
    public BalloonsModule(IResourcesLoaderService resourcesLoaderService, PopBalloonCommand.Factory balloonCommand)
    {
        _balloonCommand = balloonCommand;
        _balloonsCreator = new BalloonsCreator(resourcesLoaderService);
    }

    public void LoadData()
    {
        _balloonsData = _balloonsCreator.LoadBalloonsData();
    }
    
    public void SetupBalloons()
    {
        var balloons = GameObject.FindObjectsOfType<BalloonView>();
        var balloonsColorPalette = _balloonsData.BalloonsColorPalette;
        var balloonsColorPaletteLength = balloonsColorPalette.Length;
        
        for (int i = 0; i < balloons.Length; i++)
        {
            var randomColor = balloonsColorPalette[Random.Range(0, balloonsColorPaletteLength)];
            balloons[i].Setup(OnBalloonPopTriggered, randomColor);
        }
    }

    private void OnBalloonPopTriggered(BalloonView balloonView, Vector3 position)
    {
        _balloonCommand.Create(new PopBalloonCommandData(balloonView, position)).Execute();
    }
}
