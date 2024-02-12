using ChessGame.Scripts.ChessEngine;
using TMPro;
using UnityEngine;
namespace ChessGame.Scripts.MenuCanvas {
    public class MenuCanvas : MonoBehaviour {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private TMP_Text whoseMoveTextTMP;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject mainPanelButtons;
        [SerializeField] private TMP_Text playButtonTextTMP;
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
                playButtonTextTMP.text = "Resume";
            }
        }

        public void GameEnded(string whoWin) {
            endGamePanel.SetWhoWinText(whoWin);
            playButtonTextTMP.text = "Play";
        }

        public void SetWhoseMoveNow(bool isWhiteTurn) {
            string whoMoveText = isWhiteTurn ? "White Turn!" : "Black Turn!";
            whoseMoveTextTMP.text = whoMoveText;
            whoseMoveTextTMP.color = isWhiteTurn ? Color.white : Color.black;
        }

        public void QuitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}