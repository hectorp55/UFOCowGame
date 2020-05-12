using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    public float moveSpeed = 5f;

    public void Hover(float horizontalInput) {
        Vector3 movement = Vector3.right * Time.deltaTime * horizontalInput * moveSpeed;
        transform.Translate(movement);

        if (transform.position.x <= ScreenConstants.LeftBound) {
            transform.position = new Vector3(ScreenConstants.LeftBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= ScreenConstants.RightBound) {
            transform.position = new Vector3(ScreenConstants.RightBound, transform.position.y, transform.position.z);
        }
    }
}
