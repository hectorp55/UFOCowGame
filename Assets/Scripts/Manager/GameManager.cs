using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {
    public List<Cow> MissionCows { get; private set; }
    public List<Cow> CapturedCows { get; private set; }
    public int GoodCowCount { get; private set; }
    public int BadCowCount { get; private set; }
    public bool IsMissionSuccessful { get; private set; }
    public int MissionCount { get; private set; }
    public Dictionary<string, GameObject> CowPrefabs { get; private set; } 

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag(TagConstants.GameManager).Length == 1) {
            DontDestroyOnLoad(gameObject);

            var prefabs = Resources.LoadAll<GameObject>("Cows");
            CowPrefabs = prefabs.ToDictionary(fab => fab.name, fab => fab);
        }
        else {
            Destroy(gameObject);
        }
    }

    public static GameManager GetManager() {
        return GameObject.FindObjectOfType<GameManager>();
    }

    public void SetupNewMission(GameObject missionReport) {
        // Reset Values
        MissionCows = new List<Cow>();
        CapturedCows = new List<Cow>();
        GoodCowCount = 0;
        BadCowCount = 0;
        IsMissionSuccessful = true;
        MissionCount++;
        
        // Generate Cows for Mission
        int cowsInField = Random.Range(1, 5);
        for(int i = 0; i < cowsInField; i++) {
            SetUpCow();
        }

        // Display Report
        foreach(Cow cowToSpawn in MissionCows) {
            SpawnMissionReportCow(cowToSpawn, missionReport);
        }
    }

    public void StartNewMission(GameObject pasture) {
        // Spawn Cows in Pasture from Mission Report
        int cowsInField = Random.Range(1, 5);
        foreach(Cow cowToSpawn in MissionCows) {
            SpawnCow(cowToSpawn, pasture);
        }
    }

    public void CompleteMission() {
        IsMissionSuccessful = IsMissionSuccessful && GoodCowCount == CapturedCows.Count;

        SceneManager.LoadScene("Retry");
    }

    public void CaptureCow(GameObject cow) {
        // Add Score to Total
        CowController captueredCow = cow.GetComponent<CowController>();
        CapturedCows.Add(captueredCow.cow);
        IsMissionSuccessful = IsMissionSuccessful && captueredCow.cow.correctCow;
        Destroy(cow);
    }

    public void GameOver() {
        Destroy(gameObject);
    }

    private void SetUpCow() {
        int correctCow = Random.Range(0, 2); // (Inclusive, Exclusive) -.-
        float startingLocation = Random.Range(ScreenConstants.LeftBound, ScreenConstants.RightBound);
        int randomPrefab = Random.Range(0, CowPrefabs.Count);

        // Calculate Cow Spawn Point
        Vector3 spawnLocation = Vector3.right * startingLocation;

        // Add Cow To Mission
        Cow newCow = new Cow(correctCow == 1, spawnLocation, CowPrefabs.ElementAt(randomPrefab).Key);
        MissionCows.Add(newCow);

        // Update Cow Counts
        newCow.correctCow = correctCow > 0;
        if (correctCow > 0) {
            GoodCowCount++;
        } else {
            BadCowCount++;
        }
    }

    private GameObject SpawnCow(Cow cow, GameObject parent) {
        // Spawn Cow
        GameObject spawnedCow = Instantiate(CowPrefabs[cow.type], parent.transform);
        
        // Place in Random Spot
        spawnedCow.transform.localPosition = cow.spawnLocation;
        spawnedCow.GetComponent<CowController>().cow = cow;

        return spawnedCow;
    }

    private void SpawnMissionReportCow(Cow cow, GameObject parent) {
        GameObject spawnedCow = SpawnCow(cow, parent);

        // Destroy all components but the ones we need
        foreach(var component in spawnedCow.GetComponents<Component>()) {
            var isToBeDestroyed = component as SpriteRenderer == null && 
            component as Transform == null;
            if (isToBeDestroyed) {
                Destroy(component);
            }
        }

        // Show Correct Cows
        spawnedCow.GetComponent<SpriteRenderer>().color = cow.correctCow ? Color.green : Color.red;
    }
}
