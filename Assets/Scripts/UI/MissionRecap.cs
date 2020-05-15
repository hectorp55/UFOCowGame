using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionRecap : MonoBehaviour
{
    public Image cowIcon;
    public Text missionStatus;

    void Start()
    {
        bool isMissionSuccesful = true;

        // Display Cow Icons
        List<Cow> capturedCows = GameManager.Instance?.CapturedCows ?? new List<Cow>();
        foreach (Cow cow in capturedCows) {
            Image createdCow = Instantiate(cowIcon, gameObject.transform);
            createdCow.color = cow.correctCow ? Color.green : Color.red;
            isMissionSuccesful = isMissionSuccesful && cow.correctCow;
        }

        // Display Mission Status
        isMissionSuccesful = isMissionSuccesful && GameManager.Instance?.GoodCowCount == capturedCows.Count;
        missionStatus.text = isMissionSuccesful ? "Success" : "Failure";
    }

}
