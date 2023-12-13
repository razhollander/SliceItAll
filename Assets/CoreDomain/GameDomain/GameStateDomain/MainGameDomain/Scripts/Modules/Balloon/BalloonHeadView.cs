using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHeadView : PopableView
{
    private Action _onPopped;

    public void Setup(Action onPopped)
    {
        _onPopped = onPopped;
    }
    
    public override void Pop()
    {
        _onPopped?.Invoke();
    }
}
