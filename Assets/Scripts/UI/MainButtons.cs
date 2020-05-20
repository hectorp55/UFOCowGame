using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtons : MonoBehaviour
{
    public void ReturnHome() {
        GameManager.GetManager().CompleteMission();
    }
}
