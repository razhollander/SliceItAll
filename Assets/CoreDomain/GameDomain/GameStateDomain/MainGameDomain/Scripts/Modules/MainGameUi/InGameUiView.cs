using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class InGameUiView : MonoBehaviour
{
    [SerializeField] private Countable _scoreCountable;

    public void SetCurrentScoreText(int score, bool withAnimation = true)
    {
        if (withAnimation)
        {
            _scoreCountable.SetNumber(score);
        }
        else
        {
            _scoreCountable.SetStartingNumber(score);
        }
    }
}
