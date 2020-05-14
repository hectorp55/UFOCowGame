using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionRecap : MonoBehaviour
{
    public Image cowIcon;

    void Start()
    {
        List<Cow> capturedCows = GameManager.Instance?.CapturedCows ?? new List<Cow>();
        foreach (Cow cow in capturedCows) {
            Image createdCow = Instantiate(cowIcon, gameObject.transform);
            createdCow.color = cow.correctCow ? Color.green : Color.red;
        }
    }
}
