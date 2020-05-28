using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring
{
    public static int UpdateScore(int currentScore, bool isMissionComplete, Stack<Cow> capturedCows) {
        int missionCompletePoints = isMissionComplete ? ScoreConstants.MissionComplete : 0;
        
        int goodCowPoints = 0;
        int badCowPoints = 0;
        foreach(Cow cow in capturedCows) {
            if (cow.correctCow) {
                goodCowPoints += ScoreConstants.CorrectCow;
            } else {
                badCowPoints += ScoreConstants.CorrectCow;
            }
        }

        return currentScore + missionCompletePoints + goodCowPoints + badCowPoints;
    }

    public static bool UpdateHighScore(int currentScore) {
        int currentHighScore = GetCurrentHighScore();

        if (currentHighScore < currentScore) {
            PlayerPrefs.SetInt(ScoreConstants.HighScore, currentScore);
            return true;
        } else {
            return false;
        }
    }

    public static int GetCurrentHighScore() {
        return PlayerPrefs.HasKey(ScoreConstants.HighScore) ? PlayerPrefs.GetInt(ScoreConstants.HighScore) : 0;
    }
}
