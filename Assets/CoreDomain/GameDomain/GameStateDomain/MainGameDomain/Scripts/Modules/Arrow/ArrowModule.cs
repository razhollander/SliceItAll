using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowModule : IArrowModule
{
    public Transform ArrowTransform => _arrowView.transform;
    
    private ArrowView _arrowView;
    
    public void FindArrow()
    {
        _arrowView = GameObject.FindObjectOfType<ArrowView>();
    }
}