using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DB
{
    private static int score, lvlIndex, highScore;


    public static int HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;
        }
    }

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public static int LvlIndex
    {
        get
        {
            return lvlIndex;
        }
        set
        {
            lvlIndex = value;
        }
    }
}
