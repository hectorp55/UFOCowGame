using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow
{
    public bool correctCow;
    public string type;
    public Vector3 spawnLocation;

    public Cow(bool isCorrect, Vector3 spawnLoc, string cowType) {
        correctCow = isCorrect;
        spawnLocation = spawnLoc;
        type = cowType;
    }
}
