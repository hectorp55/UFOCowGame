using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pasture : MonoBehaviour
{
    void Start()
    {
        GameManager manager = GameManager.GetManager();
        if (manager == null) {
            SceneManager.LoadScene("Mission");
        } else {
            manager.StartNewMission(gameObject);
        }
    }  
}
