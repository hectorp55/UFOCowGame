using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullController : CowController
{
    override protected void DoWhenBeamIsTooClose() {
        Vector3 towardsBeam = ufoBeam.transform.position - transform.position;
        towardsBeam.y = 0;

        Walk(towardsBeam.normalized, runSpeed);
    }

    override protected void DoWhenBeamIsTooFar() {
        Vector3 awayFromBeam = transform.position - ufoBeam.transform.position;
        awayFromBeam.y = 0;

        Walk(awayFromBeam.normalized, runSpeed);
    }
}
