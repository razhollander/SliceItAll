using TMPro;
using UnityEngine;

public class BeforeGameUiView : MonoBehaviour
{
    private const string CurrentLevelTextFormat = "Level {0}";
    [SerializeField] private TextMeshProUGUI _currentLevelText;

    public void SetCurrentLevelText(string text)
    {
        _currentLevelText.text = string.Format(CurrentLevelTextFormat, text);
    }
}
