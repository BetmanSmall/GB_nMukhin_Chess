namespace ChessGame.Scripts {
    public class Bishop : Chessman {
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
            return r;
        }
    }
}