using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScreenButtons : MonoBehaviour
{
    public GameObject ContinueButton;

    void Start()
    {
        ContinueButton.SetActive(GameManager.Instance?.IsMissionSuccessful ?? false);
    }

    public void ContinueGame() {
        LoadScene("MainGame");
    }

    public void QuitGame() {
        LoadScene("Title");
    }

    private void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
