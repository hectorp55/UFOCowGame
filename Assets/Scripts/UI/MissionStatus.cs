using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionStatus : MonoBehaviour
{
    public Text MissionCountText;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetManager();
    }

    void Update()
    {
        MissionCountText.text = $"Mission {GameManager.GetManager().MissionCount}";

    }
}
