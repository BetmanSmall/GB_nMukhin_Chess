using ChessGame.Scripts.ChessEngine;
using TMPro;
using UnityEngine;
namespace ChessGame.Scripts.MenuCanvas {
    public class MenuCanvas : MonoBehaviour {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject mainPanelButtons;
        [SerializeField] private TMP_Text tmpTextPlayButton;
        [SerializeField] private GameObject selectedColor;
        [SerializeField] private EndGamePanel endGamePanel;

        public void StartGameOrResume() {
            if (boardManager.GameStarted) {
                PauseOrResumeGame();
            } else {
                mainPanelButtons.SetActive(false);
                selectedColor.SetActive(true);
            }
        }

        public void PauseOrResumeGame() {
            if (boardManager.GameStarted) {
                bool gamePaused = boardManager.PauseOrResumeGame();
                mainPanel.SetActive(gamePaused);
                tmpTextPlayButton.text = "Resume";
            }
        }

        public void GameEnded(string whoWin) {
            endGamePanel.SetWhoWinText(whoWin);
            tmpTextPlayButton.text = "Play";
        }
    }
}