using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoController : MonoBehaviour
{
    public Ufo ufo;

    void Update()
    {
        ListenForMovement();
        ListenForAbduction();
    }

    private void ListenForMovement() {
        ufo.Hover(Input.GetAxis(InputConstants.Horizontal));
    }

    private void ListenForAbduction() {
        if (Input.GetButtonDown(InputConstants.Abduct)) {
            ufo.BeamOn();
        }
        if (Input.GetButtonUp(InputConstants.Abduct)) {
            ufo.BeamOff();
        }
    }
}
