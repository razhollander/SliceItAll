using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHeadView : PopableView
{
    [SerializeField] private Collider _collider;
    private Action _onPopped;

    public void Setup(Action onPopped)
    {
        _onPopped = onPopped;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
    
    public override void Pop()
    {
        Debug.Log("POP!");
        _onPopped?.Invoke();
    }
}
