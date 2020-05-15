using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public void StartGame() {
        LoadScene("Mission");
    }

    private void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
