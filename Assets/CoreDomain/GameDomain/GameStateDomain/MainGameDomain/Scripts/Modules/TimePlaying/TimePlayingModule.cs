using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CoreDomain.Services;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class TimePlayingModule : ITimePlayingModule, IUpdatable
{
    public int TimePlaying => _timePlayingInt;

    private readonly TimePlayingChangedCommand.Factory _timePlayingChangedCommand;
    private readonly IUpdateSubscriptionService _updateSubscriptionService;
    private float _timePlayingFloat;
    private int _prevTimePlayingInt;
    private IDisposable _timerCoroutine;
    private CancellationToken _timerToken;
    private CancellationTokenSource _cancellationToken;
    private int _timePlayingInt;

    public TimePlayingModule(TimePlayingChangedCommand.Factory timePlayingChangedCommand, IUpdateSubscriptionService updateSubscriptionService)
    {
        _timePlayingChangedCommand = timePlayingChangedCommand;
        _updateSubscriptionService = updateSubscriptionService;
    }
    public void StartTimer()
    {
        _updateSubscriptionService.RegisterUpdatable(this);
    }

    public void ResetTimer()
    {
        _timePlayingFloat = 0;
        _timePlayingInt = 0;
        _prevTimePlayingInt = 0;
    }

    public void StopTimer()
    {
        _updateSubscriptionService.UnregisterUpdatable(this);
    }

    public void ManagedUpdate()
    {
        _timePlayingFloat += Time.deltaTime;
        _timePlayingInt = Mathf.FloorToInt(_timePlayingFloat);
        
        if (_timePlayingInt != _prevTimePlayingInt)
        {
            _prevTimePlayingInt = _timePlayingInt;
            _timePlayingChangedCommand.Create(_timePlayingInt).Execute();
        }
    }
}
