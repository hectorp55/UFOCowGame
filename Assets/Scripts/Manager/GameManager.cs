using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public List<Cow> CapturedCows { get; private set; }
    public GameObject Pasture { get; private set; }
    public GameObject CowPrefab;
 
    void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        CapturedCows = new List<Cow>();
        Pasture = GameObject.FindWithTag(TagConstants.Pasture);
    }

    void Start() {
        StartMission();
    }

    public void StartMission() {
        int cowsInField = Random.Range(1, 5);
        //Spawn Cows
        for(int i = 0; i < cowsInField; i++) {
            SpawnCow();
        }
    }

    public void CaptureCow(GameObject cow) {
        // Add Score to Total
        CapturedCows.Add(cow.GetComponent<Cow>());
        Destroy(cow);
    }

    private void SpawnCow() {
        GameObject spawnedCow = Instantiate(CowPrefab, Pasture.transform);
        int correctCow = Random.Range(0, 2); // (Inclusive, Exclusive) -.-
        float startingLocation = Random.Range(ScreenConstants.LeftBound, ScreenConstants.RightBound);
        spawnedCow.transform.position = spawnedCow.transform.position + Vector3.right * startingLocation;
        spawnedCow.GetComponent<Cow>().correctCow = correctCow > 0;
    }
}
