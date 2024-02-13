using ChessGame.Scripts.Customization;
using UnityEngine;
namespace ChessGame.Scripts.ChessEngine {
    public abstract class Chessman : MonoBehaviour {
        public int CurrentX { set; get; }
        public int CurrentY { set; get; }
        public bool isWhite;
        public ChessmanType chessmanType;

        public void SetPosition(int x, int y) {
            CurrentX = x;
            CurrentY = y;
        }

        public virtual bool[,] PossibleMoves() {
            return new bool[8, 8];
        }

        protected bool Move(int x, int y, ref bool[,] r) {
            if (x >= 0 && x < 8 && y >= 0 && y < 8) {
                Chessman c = BoardManager.Instance.GetChessman(x, y);
                if (c == null) {
                    r[x, y] = true;
                } else {
                    if (isWhite != c.isWhite) {
                        r[x, y] = true;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}