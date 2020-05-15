using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public List<Cow> CapturedCows { get; private set; }
    public int GoodCowCount { get; private set; }
    public int BadCowCount { get; private set; }
    public bool IsMissionSuccessful { get; private set; }
    public GameObject Pasture { get; private set; }
    public GameObject CowPrefab;
 
    void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        CapturedCows = new List<Cow>();
        Pasture = GameObject.FindWithTag(TagConstants.Pasture);
        IsMissionSuccessful = true;
    }

    public void StartMission() {
        int cowsInField = Random.Range(1, 5);
        //Spawn Cows
        for(int i = 0; i < cowsInField; i++) {
            SpawnCow();
        }
    }

    public void CompleteMission() {
        SceneManager.LoadScene("Retry");

        IsMissionSuccessful = IsMissionSuccessful && GoodCowCount == CapturedCows.Count;
    }

    public void CaptureCow(GameObject cow) {
        // Add Score to Total
        Cow captueredCow = cow.GetComponent<Cow>();
        CapturedCows.Add(captueredCow);
        IsMissionSuccessful = IsMissionSuccessful && captueredCow.correctCow;
        Destroy(cow);
    }

    private void SpawnCow() {
        // Spawn Cow
        GameObject spawnedCow = Instantiate(CowPrefab, Pasture.transform);
        int correctCow = Random.Range(0, 2); // (Inclusive, Exclusive) -.-
        float startingLocation = Random.Range(ScreenConstants.LeftBound, ScreenConstants.RightBound);
        
        // Place in Random Spot
        spawnedCow.transform.position = spawnedCow.transform.position + Vector3.right * startingLocation;
        
        // Update Cow Counts
        spawnedCow.GetComponent<Cow>().correctCow = correctCow > 0;
        if (correctCow > 0) {
            GoodCowCount++;
        } else {
            BadCowCount++;
        }
    }
}
