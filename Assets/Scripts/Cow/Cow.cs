using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow
{
    public bool correctCow;
    public Vector3 spawnLocation;

    public Cow(bool isCorrect, Vector3 spawnLoc) {
        correctCow = isCorrect;
        spawnLocation = spawnLoc;
    }
}
