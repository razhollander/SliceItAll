using System;
using UnityEngine;
using TMPro;
public class GameOverUiView : MonoBehaviour
{
    private readonly string PassedHighScoreMessageFormat = @"Congratulations, You Broke Your High Score!" + Environment.NewLine + "New HighScore: {0}";
    private const string DidntPassHighScoreMessageFormat = "You Didn't Pass Your HighScore of {0} :(";
    private const string ScoreMessageFormat = "Score: {0}";
    private const string TimeMessageFormat = "Seconds Played: {0}";
    private const string AsteroidsPassedMessageFormat = "Asteroids Passed: {0}";
    
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timePlayedText;
    [SerializeField] private TextMeshProUGUI _asteroidsPassedText;
    [SerializeField] private TextMeshProUGUI _highScoreMessageText;

    public void SetAllTexts(
        int score,
        int timePlayed,
        int asteroidsPassed,
        bool isNewHighScore,
        int highScore)
    {
        _scoreText.text = string.Format(ScoreMessageFormat, score);
        _timePlayedText.text = string.Format(TimeMessageFormat, timePlayed);
        _asteroidsPassedText.text = string.Format(AsteroidsPassedMessageFormat, asteroidsPassed);
        _highScoreMessageText.text = isNewHighScore ? string.Format(PassedHighScoreMessageFormat, highScore) : string.Format(DidntPassHighScoreMessageFormat, highScore);
    }
}
