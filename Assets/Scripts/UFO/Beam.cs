using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public const float liftSpeed = 5f;

    public List<Rigidbody2D> CowsInBeam;


    void Update() {
        foreach(var cow in CowsInBeam) {
            liftCow(cow);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var cowToLift = other.gameObject.GetComponent<Rigidbody2D>();
		other.gameObject.GetComponent<CowController>().Moo();
        other.gameObject.GetComponent<Animator>().SetBool(AnimatorConstants.IsSpooked, true);
        CowsInBeam.Add(cowToLift);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Animator>().SetBool(AnimatorConstants.IsSpooked, false);
        CowsInBeam.Remove(other.gameObject.GetComponent<Rigidbody2D>());
    }

    private void liftCow(Rigidbody2D cowRigidbody) {
        Vector3 liftVector = gameObject.transform.parent.position - cowRigidbody.transform.position;
        cowRigidbody.velocity = liftVector.normalized * liftSpeed;
    }
}
