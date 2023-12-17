using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBalloonCommandData
{
    public BalloonView BalloonView;
    public Vector3 Position;

    public PopBalloonCommandData(BalloonView balloonView, Vector3 position)
    {
        BalloonView = balloonView;
        Position = position;
    }
}
