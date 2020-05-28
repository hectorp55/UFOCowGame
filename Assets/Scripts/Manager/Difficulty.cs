using UnityEngine;
using System.Collections;

public class Difficulty
{
    private static int MaxCowCount = 8;
    private static float MaxCowSpeed = 8f;
    private static float MinCowWaitTime = 0.5f;

    public static int CowCount(int level) {
        int count = Mathf.FloorToInt(level * 0.4f + 2f);

        return count > MaxCowCount ? MaxCowCount : count;
    }

    public static float CowSpeed(int level) {
        float speed = level * 0.233f + 3f;

        return speed > MaxCowSpeed ? MaxCowSpeed : speed;
    }

    public static Range CowWaitTime(int level) {
        float cycle = level * -0.07f + 3f;

        return cycle < MinCowWaitTime ? new Range(MinCowWaitTime, MinCowWaitTime + 1) : new Range(cycle, cycle + 1);
    }
}
