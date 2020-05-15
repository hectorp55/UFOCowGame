using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionRecap : MonoBehaviour
{
    public Image cowIcon;
    public Text missionStatus;

    void Start()
    {
        // Display Cow Icons
        List<Cow> capturedCows = GameManager.Instance?.CapturedCows ?? new List<Cow>();
        foreach (Cow cow in capturedCows) {
            Image createdCow = Instantiate(cowIcon, gameObject.transform);
            createdCow.color = cow.correctCow ? Color.green : Color.red;
        }

        // Display Mission Status
        missionStatus.text = GameManager.Instance?.IsMissionSuccessful ?? false ? "Success" : "Failure";
    }

}
