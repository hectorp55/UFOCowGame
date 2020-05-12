using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public const float liftSpeed = 3f;

    public bool isCowInRange;
    public Rigidbody2D cowToLift;

    void Update() {
        if (isCowInRange) {
            liftCow(cowToLift);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cowToLift = other.gameObject.GetComponent<Rigidbody2D>();
        isCowInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        cowToLift = null;
        isCowInRange = false;
    }

    private void liftCow(Rigidbody2D cowRigidbody) {
        cowRigidbody.velocity = Vector2.up * liftSpeed;
    }
}
