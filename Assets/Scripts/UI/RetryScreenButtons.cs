using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScreenButtons : MonoBehaviour
{
    public GameObject ContinueButton;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetManager();

        ContinueButton.SetActive(manager?.IsMissionSuccessful ?? false);
    }

    public void ContinueGame() {
        LoadScene("Mission");
    }

    public void QuitGame() {
        manager.GameOver();
        LoadScene("Title");
    }

    private void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
