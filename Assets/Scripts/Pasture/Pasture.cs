using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasture : MonoBehaviour
{
    void Start()
    {
        GameManager manager = GameManager.GetManager();
        manager?.StartNewMission(gameObject);
    }  
}
