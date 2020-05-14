using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public void ReturnHome() {
        LoadScene("Retry");
    }

    private void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
