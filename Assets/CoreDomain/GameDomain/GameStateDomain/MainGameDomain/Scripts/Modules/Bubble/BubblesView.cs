using System;
using UnityEngine;

public class BubblesView : PopableView
{
    private Action<Vector3> _onBubblePopTriggered;

    public void Setup(Action<Vector3> onBubblePopTriggered)
    {
        _onBubblePopTriggered = onBubblePopTriggered;
    }

    public override void Pop(Vector3 position)
    {
        _onBubblePopTriggered?.Invoke(position);
    }
}
