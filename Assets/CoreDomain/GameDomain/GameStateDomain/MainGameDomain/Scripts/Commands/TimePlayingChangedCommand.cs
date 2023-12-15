using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class TimePlayingChangedCommand : CommandSyncOneParameter<int, TimePlayingChangedCommand>
{
    private int _timePlaying;
    private readonly IMainGameUiModule _mainGameUiModule;

    public TimePlayingChangedCommand(int timePlaying, IMainGameUiModule mainGameUiModule)
    {
        _timePlaying = timePlaying;
        _mainGameUiModule = mainGameUiModule;
    }
    
    public override void Execute()
    {
    }
}
