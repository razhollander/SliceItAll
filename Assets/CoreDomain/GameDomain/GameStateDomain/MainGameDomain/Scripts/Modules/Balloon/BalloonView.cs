using System;
using UnityEngine;
using DG.Tweening;

public class BalloonView : PopableView
{
    [SerializeField] private Collider _collider;
    [SerializeField] private BalloonHeadView _balloonHeadView;
    [SerializeField] private GameObject _balloonString;
    [SerializeField] private ParticleSystem _popParticleSystem;
    
    private Action<BalloonView, Vector3> _onPoppedAction;

    public void Setup(Action<BalloonView, Vector3> onPoppedAction, Color color)
    {
        _onPoppedAction = onPoppedAction;
        _balloonHeadView.SetColor(color);
    }
    
    public override void Pop(Vector3 position)
    {
        _onPoppedAction?.Invoke(this, position);
    }

    public void PlayPopEffect()
    {
        _collider.enabled = false;
        _balloonHeadView.gameObject.SetActive(false);
        _balloonString.transform.DOScaleZ(0, 1.5f * _balloonString.transform.localScale.z).SetEase(Ease.OutCubic);
        _popParticleSystem.Play();
    }
}
