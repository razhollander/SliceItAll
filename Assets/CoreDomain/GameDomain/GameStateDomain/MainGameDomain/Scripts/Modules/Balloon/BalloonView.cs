using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonView : MonoBehaviour
{
    [SerializeField] private BalloonHeadView _balloonHead;
    [SerializeField] private GameObject _balloonString;
    [SerializeField] private ParticleSystem _popParticleSystem;

    private void Awake()
    {
        _balloonHead.Setup(OnPopped);
    }

    public void OnPopped()
    {
        _balloonHead.DisableCollider();
        _balloonHead.gameObject.SetActive(false);
        _balloonString.transform.DOScaleZ(0, 1.5f * _balloonString.transform.localScale.z).SetEase(Ease.OutCubic);
        _popParticleSystem.Play();
    }
}
