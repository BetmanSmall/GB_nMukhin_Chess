using TMPro;
using UnityEngine;
namespace ChessGame.Scripts.MenuCanvas {
    public class EndGamePanel : MonoBehaviour {
        [SerializeField] private TMP_Text tmpTextWhoWins;

        public void SetWhoWinText(string whoWinText) {
            tmpTextWhoWins.text = whoWinText;
            gameObject.SetActive(true);
        }
    }
}