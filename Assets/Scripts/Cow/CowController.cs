using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public float safeBeamDistanceFar = 10f;
    public float safeBeamDistanceClose = 5f;
    public float moveSpeed = 3f;
    public Cow cow;
    AudioSource mooAudioSource;

    private float cycleTimer;
    private float waitDuration;
    private float actionDuration;
    private CowActions cowAction;
    private bool isGrounded;
    private GameObject ufoBeam;

    void Awake() {
        int currentLevel = GameManager.GetManager().MissionCount;
        Range waitDurationRange = Difficulty.CowWaitTime(currentLevel);

        waitDuration = Random.Range(waitDurationRange.Min, waitDurationRange.Max);
        actionDuration = Random.Range(1f, 2f);
        cowAction = GetRandomCowAction();
        isGrounded = true;
        moveSpeed = Difficulty.CowSpeed(currentLevel);
    }

    void Start() {
        mooAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        UpdateMovementCycle();
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag(TagConstants.Floor)) {
                isGrounded = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag(TagConstants.Floor)) {
                isGrounded = false;
        }
    }

    public void Moo() {
        mooAudioSource.Play(0);
    }

    public void UpdateMovementCycle() {
        // If cow is on the ground
        if (isGrounded) {
            // If Beam is ON within range runaway to safe distance
            if (IsBeamToClose()) {
                RunAwayFromBeam();
                return;
            }
            if (IsBeamToFar()) {
                RunTowardsBeam();
                return;
            }
            
            // Cow action cycle
            cycleTimer += Time.deltaTime;
            if(cycleTimer > waitDuration + actionDuration) {
                // Restart Cycle
                cycleTimer = 0;
                cowAction = GetRandomCowAction();
            } 
            // Do Random Cow Action
            else if (cycleTimer > waitDuration && cycleTimer < waitDuration + actionDuration) {
                switch(cowAction) {
                    case CowActions.EatGrass:
                    EatGrass();
                    break;
                    case CowActions.WalkLeft:
                    WalkLeft();
                    break;
                    case CowActions.WalkRight:
                    WalkRight();
                    break;
                }
            }
        }
    }

    private bool IsBeamToClose() {
        ufoBeam = GameObject.FindWithTag(TagConstants.Beam);

        return ufoBeam != null && ufoBeam.activeSelf && Vector3.Distance(ufoBeam.transform.position, transform.position) < safeBeamDistanceClose;
    }

    private bool IsBeamToFar() {
        ufoBeam = GameObject.FindWithTag(TagConstants.Beam);

        return ufoBeam != null && ufoBeam.activeSelf && Vector3.Distance(ufoBeam.transform.position, transform.position) > safeBeamDistanceFar;
    }

    private void WalkLeft() {
        Walk(Vector3.left);
    }

    private void WalkRight() {
        Walk(Vector3.right);
    }

    private void EatGrass() {
        // Trigger Eat Grass Animation
    }

    private void RunAwayFromBeam() {
        Vector3 awayFromBeam = transform.position - ufoBeam.transform.position;
        awayFromBeam.y = 0;

        Walk(awayFromBeam.normalized);
    }

    private void RunTowardsBeam() {
        Vector3 towardsBeam = ufoBeam.transform.position - transform.position;
        towardsBeam.y = 0;

        Walk(towardsBeam.normalized);
    }

    private void Walk(Vector3 direction) {
        Vector3 movement = direction * Time.deltaTime * moveSpeed;
        transform.Translate(movement);

        if (transform.position.x <= ScreenConstants.LeftBound) {
            transform.position = new Vector3(ScreenConstants.RightBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= ScreenConstants.RightBound) {
            transform.position = new Vector3(ScreenConstants.LeftBound, transform.position.y, transform.position.z);
        }
    }

    private CowActions GetRandomCowAction() {
        System.Array actions = System.Enum.GetValues(typeof(CowActions));
        return (CowActions)actions.GetValue(Random.Range(0, actions.Length));
    }
}
