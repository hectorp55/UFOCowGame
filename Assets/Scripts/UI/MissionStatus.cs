using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionStatus : MonoBehaviour
{
    public Text MissionCountText;
    public Image LastCapturedCowIcon;

    private GameManager manager;

    void Start()
    {
        manager = GameManager.GetManager();
    }

    void Update()
    {
        MissionCountText.text = $"Mission {GameManager.GetManager()?.MissionCount}";
    }

    public void UpdateInventoryCow(Cow lastCow) {
        if (lastCow == null) {
            LastCapturedCowIcon.color = Color.clear;
            LastCapturedCowIcon.sprite = null;
        } else {
            LastCapturedCowIcon.color = Color.white;
            LastCapturedCowIcon.sprite = manager.CowPrefabs[lastCow.type].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
