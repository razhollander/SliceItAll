using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChangedCommandData
{
    public float ScoreAdded;

    public ScoreChangedCommandData(float scoreAdded)
    {
        ScoreAdded = scoreAdded;
    }
}
