using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHeadView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
