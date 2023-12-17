using System;
using CoreDomain.Utils.Pools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreGainedFXView : MonoBehaviour, IPoolable
{
    private const string TextEffectFormat = "+{0}";
    
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private float _enterDurationSeconds;
    [SerializeField] private float _idleDurationSeconds;
    [SerializeField] private float _exitDurationSeconds;
    [SerializeField] private float _scale;
    [SerializeField] private Ease _enterEase = Ease.OutBounce;
    [SerializeField] private Ease _exitEase = Ease.InCubic;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void Setup(int scoreGained, Vector3 position)
    {
        _transform.position = position;
        _transform.localScale = Vector3.zero;
        _text.text = string.Format(TextEffectFormat, scoreGained.ToString());
    }
    
    public async UniTask DoShowAnimation()
    {
        await transform.DOScale(_scale, _enterDurationSeconds).SetEase(_enterEase);
        await UniTask.Delay((int) _idleDurationSeconds * 1000);
        await transform.DOScale(Vector3.zero, _exitDurationSeconds).SetEase(_exitEase);
    }

    public Action Despawn { get; set; }
    
    public void OnSpawned()
    {
        gameObject.SetActive(true);
    }

    public void OnDespawned()
    {
        gameObject.SetActive(false);
    }
}
