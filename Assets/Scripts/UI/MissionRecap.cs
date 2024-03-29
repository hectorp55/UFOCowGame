﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionRecap : MonoBehaviour
{
    public Image cowIcon;
    public Text missionStatus;
    public Text currentScore;
    public Text highScore;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetManager();

        // Display Cow Icons
        Stack<Cow> capturedCows = manager?.CapturedCows ?? new Stack<Cow>();
        foreach (Cow cow in capturedCows) {
            Image createdCow = Instantiate(cowIcon, gameObject.transform);
            createdCow.color = cow.correctCow ? Color.green : Color.red;
        }

        // Display Mission Status
        string status = manager?.IsMissionSuccessful ?? false ? "Success" : "Failure";
        missionStatus.text = $"Mission {manager?.MissionCount.ToString() ?? ""} {status}";
        currentScore.text = manager?.CurrentScore.ToString() ?? "0";

        // Display Highscore
        bool isCurrentHighscore = Scoring.UpdateHighScore(manager?.CurrentScore ?? 0);
        highScore.text = Scoring.GetCurrentHighScore().ToString();
        if (isCurrentHighscore) highScore.color = Color.green;
    }

}
