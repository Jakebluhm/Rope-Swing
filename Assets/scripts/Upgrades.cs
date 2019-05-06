using UnityEngine;
using UnityEditor;

public static class Upgrades
{
    private static int glider;


    public static int Glider
    {
        get
        {
            return glider;
        }
        set
        {
            glider = value;
        }
    }


}
