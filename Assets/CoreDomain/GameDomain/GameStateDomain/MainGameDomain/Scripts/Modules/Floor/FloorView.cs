using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _cornerPoint;
    
    public Transform StartPoint => _startPoint;
    public Transform CornerPoint => _cornerPoint;

    private Material _floorMaterial;
    private Vector2 _currentTextureOffset=Vector2.zero;
    private float _floorTextureScale;
    private float _floorYScale;
    
    private void Awake()
    {
        _floorMaterial = _renderer.sharedMaterial;
        _floorTextureScale = _floorMaterial.mainTextureScale.y;
        _floorYScale = transform.localScale.y; // y and not z because the floor is rotated by 90 degrees 
    }

    public void ChangeOffset(float offsetDelta)
    {
        _currentTextureOffset = CalcNewOffset(offsetDelta);
        _floorMaterial.mainTextureOffset = _currentTextureOffset;
    }

    private Vector2 CalcNewOffset(float offsetDelta)
    {
        var newOffset = offsetDelta*_floorTextureScale/_floorYScale + _currentTextureOffset.y;

        if (newOffset > 1f)
            newOffset %= 1f;

        return new Vector2(0, newOffset);
    }
}
