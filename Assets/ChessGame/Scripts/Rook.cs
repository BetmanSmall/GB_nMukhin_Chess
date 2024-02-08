using System.Collections;
using UnityEngine;

public class Rook : Chessman {
    public override bool[,] PossibleMoves() {
        bool[,] r = new bool[8, 8];
        // Right
        int x = CurrentX;
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
        int y = CurrentY;
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