using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public const float liftSpeed = 5f;

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
		other.gameObject.GetComponent<CowController>().Moo();
        other.gameObject.GetComponent<Animator>().SetBool(AnimatorConstants.IsSpooked, true);
        isCowInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        cowToLift = null;
        other.gameObject.GetComponent<Animator>().SetBool(AnimatorConstants.IsSpooked, false);
        isCowInRange = false;
    }

    private void liftCow(Rigidbody2D cowRigidbody) {
        Vector3 liftVector = gameObject.transform.parent.position - cowRigidbody.transform.position;
        cowRigidbody.velocity = liftVector.normalized * liftSpeed;
    }
}
