using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class WinUiView : MonoBehaviour
{
    [SerializeField] private Countable _scoreCountable;

    public void SetScoreText(int score)
    {
        _scoreCountable.SetStartingNumber(score);
        _scoreCountable.SetNumber(score);
    }
}
