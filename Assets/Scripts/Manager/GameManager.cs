using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float cowsInField;
    public GameObject pasture;
    public GameObject cowPrefab;

    void Awake() {
        cowsInField = Random.Range(1, 5);
    }

    void Start() {
        StartMission();
    }

    private void StartMission() {
        //Spawn Cows
        for(int i = 0; i < cowsInField; i++) {
            SpawnCow();
        }
    }

    private void SpawnCow() {
        GameObject spawnedCow = Instantiate(cowPrefab, pasture.transform);
        float startingLocation = Random.Range(ScreenConstants.LeftBound, ScreenConstants.RightBound);
        spawnedCow.transform.position = spawnedCow.transform.position + Vector3.right * startingLocation;
    }
}
