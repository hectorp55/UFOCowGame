using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScreenButtons : MonoBehaviour
{
    public void RetryGame() {
        LoadScene("MainGame");
    }

    private void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
