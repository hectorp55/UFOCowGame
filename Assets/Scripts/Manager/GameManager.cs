using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public List<Cow> CapturedCows { get; private set; }
    public int GoodCowCount { get; private set; }
    public int BadCowCount { get; private set; }
    public bool IsMissionSuccessful { get; private set; }
    public int MissionCount { get; private set; }

    public GameObject CowPrefab; 

    void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length == 1) {
            DontDestroyOnLoad(gameObject); 
        }
        else {
            Destroy(gameObject); 
        }
    }

    public static GameManager GetManager() {
        return GameObject.FindObjectOfType<GameManager>();
    }

    public void StartNewMission() {
        // Reset Values
        CapturedCows = new List<Cow>();
        IsMissionSuccessful = true;
        MissionCount++;
        
        // Spawn Cows
        int cowsInField = Random.Range(1, 5);
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

    public void GameOver() {
        Destroy(gameObject);
    }

    private void SpawnCow() {
        // Spawn Cow
        GameObject pasture = GameObject.FindWithTag(TagConstants.Pasture);
        GameObject spawnedCow = Instantiate(CowPrefab, pasture.transform);
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
