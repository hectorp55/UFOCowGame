using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionReport : MonoBehaviour
{
    void Start()
    {
        GameManager.GetManager().SetupNewMission(gameObject);
    }
}
