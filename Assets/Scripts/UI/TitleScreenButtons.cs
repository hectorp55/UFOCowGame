using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public void StartGame() {
        LoadScene("Main");
    }

    private void LoadScene(string sceneName) {
        SceneManager.LoadScene("Main");
    }
}
