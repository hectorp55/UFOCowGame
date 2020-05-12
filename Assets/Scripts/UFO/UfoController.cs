using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoController : MonoBehaviour
{
    public Ufo ufo;

    void Update()
    {
        ufo.Hover(Input.GetAxis("Horizontal"));
    }
}
