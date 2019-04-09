using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DB
{
    private static int score, lvlIndex;

    
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
