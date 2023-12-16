using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonView : PopableView
{
    [SerializeField] private Collider _collider;
    [SerializeField] private BalloonHeadView _balloonHeadView;
    [SerializeField] private GameObject _balloonString;
    [SerializeField] private ParticleSystem _popParticleSystem;
    
    public override void Pop()
    {
        _collider.enabled = false;
        _balloonHeadView.gameObject.SetActive(false);
        _balloonString.transform.DOScaleZ(0, 1.5f * _balloonString.transform.localScale.z).SetEase(Ease.OutCubic);
        _popParticleSystem.Play();
    }

    public override Vector3 GetCenterPoint()
    {
        return _balloonHeadView.transform.position;
    }
}
