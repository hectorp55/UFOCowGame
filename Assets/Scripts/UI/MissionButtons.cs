using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionButtons : MonoBehaviour
{
    public void StartNewMission() {
        SceneManager.LoadScene("MainGame");
    }
}
