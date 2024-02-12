namespace ChessGame.Scripts.ChessEngine {
    public class Queen : Chessman {
        public override bool[,] PossibleMoves() {
            bool[,] r = new bool[8, 8];
            // Top left
            int x = CurrentX;
            int y = CurrentY;
            while (true) {
                x--;
                y++;
                if (x < 0 || y >= 8) break;
                if (Move(x, y, ref r)) break;
            }
            // Top right
            x = CurrentX;
            y = CurrentY;
            while (true) {
                x++;
                y++;
                if (x >= 8 || y >= 8) break;
                if (Move(x, y, ref r)) break;
            }
            // Down left
            x = CurrentX;
            y = CurrentY;
            while (true) {
                x--;
                y--;
                if (x < 0 || y < 0) break;
                if (Move(x, y, ref r)) break;
            }
            // Down right
            x = CurrentX;
            y = CurrentY;
            while (true) {
                x++;
                y--;
                if (x >= 8 || y < 0) break;
                if (Move(x, y, ref r)) break;
            }
            // Right
            x = CurrentX;
            while (true) {
                x++;
                if (x >= 8) break;
                if (Move(x, CurrentY, ref r)) break;
            }
            // Left
            x = CurrentX;
            while (true) {
                x--;
                if (x < 0) break;
                if (Move(x, CurrentY, ref r)) break;
            }
            // Up
            y = CurrentY;
            while (true) {
                y++;
                if (y >= 8) break;
                if (Move(CurrentX, y, ref r)) break;
            }
            // Down
            y = CurrentY;
            while (true) {
                y--;
                if (y < 0) break;
                if (Move(CurrentX, y, ref r)) break;
            }
            return r;
        }
    }
}