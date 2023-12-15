using System;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;
using TMPro;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiView : MonoBehaviour
    {
        private const string CurrentLevelTextFormat = "Level {0}";
        
        [SerializeField] private Countable _scoreCountable;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private GameObject _inGamePanel;
        [SerializeField] private GameObject _beforeGamePanel;
        [SerializeField] private GameObject _gameOverPanel;

        public void ShowGameOverPanel()
        {
            _gameOverPanel.gameObject.SetActive(true);
        }
        
        public void HideGameOverPanel()
        {
            _gameOverPanel.gameObject.SetActive(false);
        }

        public void UpdateScore(int newScore)
        {
            _scoreCountable.SetNumber(newScore);
        }
        
        public void SwitchToInGameView()
        {
            _inGamePanel.SetActive(true);
            _beforeGamePanel.SetActive(false);
        }
        
        public void SwitchToBeforeGameView(int currentLevel)
        {
            _currentLevelText.text = string.Format(CurrentLevelTextFormat, currentLevel);
            _beforeGamePanel.SetActive(true);
            _inGamePanel.SetActive(false);
            _gameOverPanel.SetActive(false);
        }

        public void SetStartingValues(int score)
        {
            _scoreCountable.SetStartingValue(score);
        }
    }
}