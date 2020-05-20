using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionRecap : MonoBehaviour
{
    public Image cowIcon;
    public Text missionStatus;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetManager();

        // Display Cow Icons
        List<Cow> capturedCows = manager?.CapturedCows ?? new List<Cow>();
        foreach (Cow cow in capturedCows) {
            Image createdCow = Instantiate(cowIcon, gameObject.transform);
            createdCow.color = cow.correctCow ? Color.green : Color.red;
        }

        // Display Mission Status
        string status = manager?.IsMissionSuccessful ?? false ? "Success" : "Failure";
        missionStatus.text = $"Mission {manager?.MissionCount ?? 1} {status}";
    }

}
