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
    
    private Action<BalloonView> _onPoppedAction;

    public void Setup(Action<BalloonView> onPoppedAction, Color color)
    {
        _onPoppedAction = onPoppedAction;
        _balloonHeadView.SetColor(color);
    }
    
    public override void Pop()
    {
        _onPoppedAction?.Invoke(this);
    }

    public void PlayPopEffect()
    {
        _collider.enabled = false;
        _balloonHeadView.gameObject.SetActive(false);
        _balloonString.transform.DOScaleZ(0, 1.5f * _balloonString.transform.localScale.z).SetEase(Ease.OutCubic);
        _popParticleSystem.Play();
    }

    public override Vector3 GetPopCenterPoint()
    {
        return _balloonHeadView.transform.position;
    }
}
