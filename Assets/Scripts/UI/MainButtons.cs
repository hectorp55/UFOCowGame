using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtons : MonoBehaviour
{
    public GameObject ufo;

    public void ReturnHome() {
        ufo.GetComponent<Ufo>().FlyHome();
    }
}
