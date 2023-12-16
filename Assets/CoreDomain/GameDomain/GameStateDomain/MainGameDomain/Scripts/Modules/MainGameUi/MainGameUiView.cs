using System;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;
using TMPro;

namespace CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi
{
    public class MainGameUiView : MonoBehaviour
    {
        
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private BeforeGameUiView _beforeGameUiView;
        [SerializeField] private InGameUiView _inGameUiView;
        [SerializeField] private WinUiView _winUiView;
        
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
            _inGameUiView.SetCurrentScoreText(newScore);
        }
        
        public void SwitchToInGameView()
        {
            _inGameUiView.gameObject.SetActive(true);
            _beforeGameUiView.gameObject.SetActive(false);
        }
        
        public void SwitchToBeforeGameView(int currentLevel)
        {
            _beforeGameUiView.SetCurrentLevelText(currentLevel.ToString());
            _beforeGameUiView.gameObject.SetActive(true);
            _inGameUiView.gameObject.SetActive(false);
            _gameOverPanel.SetActive(false);
            _winUiView.gameObject.SetActive(false);
        }

        public void SetStartingValues(int score)
        {
            _inGameUiView.SetCurrentScoreText(score, false);
        }

        public void ShowWinPanel(int winScore)
        {
            _winUiView.gameObject.SetActive(true);
            _winUiView.SetScoreText(winScore);
        }
    }
}