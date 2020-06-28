using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {
    public List<Cow> MissionCows { get; private set; }
    public Stack<Cow> CapturedCows { get; private set; }
    public int GoodCowCount { get; private set; }
    public int BadCowCount { get; private set; }
    public bool IsMissionSuccessful { get; private set; }
    public int MissionCount { get; private set; }
    public Dictionary<string, GameObject> CowPrefabs { get; private set; } 
    public List<Vector3> PossibleSpawnLocations;
    public List<Vector3> MissionSpawnLocations { get; private set; } 
    public int CurrentScore { get; private set; }
    public GameObject Pointer;

    void Awake()
    {
        // Initialize
        if (GameObject.FindGameObjectsWithTag(TagConstants.GameManager).Length == 1) {
            DontDestroyOnLoad(gameObject);

            var prefabs = Resources.LoadAll<GameObject>("Cows");
            CowPrefabs = prefabs.ToDictionary(fab => fab.name, fab => fab);

            PossibleSpawnLocations = new List<Vector3>();
            for (float i = ScreenConstants.LeftBound; i <= ScreenConstants.RightBound; i+=ScreenConstants.CowSpawnSpace) {
                PossibleSpawnLocations.Add(Vector3.right * i);
            }

            CurrentScore = 0;
        }
        // Destroy
        else {
            Destroy(gameObject);
        }
    }

    public static GameManager GetManager() {
        GameObject manager = GameObject.FindWithTag(TagConstants.GameManager);
        
        if (manager == null) {
            SceneManager.LoadScene("Mission");
            return null;
        } else {
            return manager.GetComponent<GameManager>();
        }
    }

    public void SetupNewMission(GameObject missionReport) {
        // Reset Values
        MissionCows = new List<Cow>();
        CapturedCows = new Stack<Cow>();
        GoodCowCount = 0;
        BadCowCount = 0;
        IsMissionSuccessful = true;
        MissionCount++;
        MissionSpawnLocations = new List<Vector3>(PossibleSpawnLocations);
        
        // Generate Cows for Mission
        int cowsInField = Difficulty.CowCount(MissionCount);
        for(int i = 0; i < cowsInField; i++) {
            // Add Cow To Mission
            Cow setCow = SetUpCow();
            MissionCows.Add(setCow);
        }

        // Display Report
        foreach(Cow cowToSpawn in MissionCows) {
            SpawnMissionReportCow(cowToSpawn, missionReport);
        }
    }

    public void StartNewMission(GameObject pasture) {
        // Spawn Cows in Pasture from Mission Report
        foreach(Cow cowToSpawn in MissionCows) {
            SpawnCow(cowToSpawn, pasture);
        }
    }

    public void CompleteMission() {
        foreach (Cow cow in CapturedCows) {
            IsMissionSuccessful = IsMissionSuccessful && cow.correctCow;
        }
        IsMissionSuccessful = IsMissionSuccessful && GoodCowCount == CapturedCows.Count;
        
        // Update Current Score
        CurrentScore = Scoring.UpdateScore(CurrentScore, IsMissionSuccessful, CapturedCows);

        SceneManager.LoadScene("Retry");
    }

    public void CaptureCow(GameObject cow) {
        CowController captueredCow = cow.GetComponent<CowController>();
        CapturedCows.Push(captueredCow.cow);
        Destroy(cow);

        GameObject.FindWithTag(TagConstants.MissionStatus).GetComponent<MissionStatus>().UpdateInventoryCow(captueredCow.cow);
    }

    public void ThrowBackCow(Vector3 throwbackSpot) {
        if (CapturedCows.Count == 0) {
            return;
        }

        Cow throwBack = CapturedCows.Pop();
        throwBack.spawnLocation = throwbackSpot;

        Cow nextCow = CapturedCows.Count == 0 ? null : CapturedCows.Peek();
        GameObject.FindWithTag(TagConstants.MissionStatus).GetComponent<MissionStatus>().UpdateInventoryCow(nextCow);

        SpawnCow(throwBack, GameObject.FindWithTag(TagConstants.Pasture));
    }

    public void GameOver() {
        Destroy(gameObject);
    }

    private Cow SetUpCow() {
        // The first cow is always correct and the second cow is always wrong
        int correctCow = MissionCows.Count == 0 ? 1 : MissionCows.Count == 1 ? 0 : Random.Range(0, 2);
        int startingLocation = Random.Range(0, MissionSpawnLocations.Count);
        int randomPrefab = Random.Range(0, CowPrefabs.Count);

        // Get Cow Spawn Point and Remove from List
        Vector3 spawnLocation = MissionSpawnLocations[startingLocation];
        MissionSpawnLocations.RemoveAt(startingLocation);

        // Create Cow
        Cow newCow = new Cow(correctCow == 1, spawnLocation, CowPrefabs.ElementAt(randomPrefab).Key);

        // Update Cow Counts
        newCow.correctCow = correctCow > 0;
        if (correctCow > 0) {
            GoodCowCount++;
        } else {
            BadCowCount++;
        }

        return newCow;
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
        if (cow.correctCow) {
            Instantiate(Pointer, spawnedCow.transform);
        }
    }
}
