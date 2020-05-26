using UnityEngine;
using System.Collections;

public class Difficulty
{
    public static int CowCount(int level) {
        return Mathf.FloorToInt(level * 0.2f + 2f);
    }

    public static float CowSpeed(int level) {
        return level * 0.233f + 3f;
    }
}
