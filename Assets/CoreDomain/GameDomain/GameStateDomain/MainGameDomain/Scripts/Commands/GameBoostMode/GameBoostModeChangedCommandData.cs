using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoostModeChangedCommandData
{
    public bool IsBoostOn;

    public GameBoostModeChangedCommandData(bool isBoostOn)
    {
        IsBoostOn = isBoostOn;
    }
}
