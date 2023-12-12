using System.Collections;
using System.Collections.Generic;
using CoreDomain.Scripts.Services.DataPersistence;
using UnityEngine;

public class HighScoreModule : IHighScoreModule
{
    private readonly IDataPersistence _dataPersistence;
    private const string HighScoreSaveKey = "HighScore";
    public  int LastHighScore { get; private set; }
    
    public HighScoreModule(IDataPersistence dataPersistence)
    {
        _dataPersistence = dataPersistence;
    }

    public void LoadLastHighScore()
    {
        LastHighScore = _dataPersistence.Load<int>(HighScoreSaveKey);
    }

    public void SaveHighScore(int highScore)
    {
        _dataPersistence.Save(HighScoreSaveKey, highScore);
        LastHighScore = highScore;
    }
}
