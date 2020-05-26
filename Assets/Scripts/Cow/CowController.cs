using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public float safeBeamDistance = 5f;
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
        waitDuration = Random.Range(2f, 5f);
        actionDuration = Random.Range(1f, 2f);
        cowAction = GetRandomCowAction();
        isGrounded = true;
        moveSpeed = Difficulty.CowSpeed(GameManager.GetManager().MissionCount);
    }

    void Start() {
        mooAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        // If cow is on the ground
        if (isGrounded) {
            // If Beam is ON within range runaway to safe distance
            if (IsBeamToClose()) {
                RunAwayFromBeam();
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

    private bool IsBeamToClose() {
        ufoBeam = GameObject.FindWithTag(TagConstants.Beam);

        return ufoBeam != null && ufoBeam.activeSelf && Vector3.Distance(ufoBeam.transform.position, transform.position) < safeBeamDistance;
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

    private void Walk(Vector3 direction) {
        Vector3 movement = direction * Time.deltaTime * moveSpeed;
        transform.Translate(movement);

        if (transform.position.x <= ScreenConstants.LeftBound) {
            transform.position = new Vector3(ScreenConstants.LeftBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= ScreenConstants.RightBound) {
            transform.position = new Vector3(ScreenConstants.RightBound, transform.position.y, transform.position.z);
        }
    }

    private CowActions GetRandomCowAction() {
        System.Array actions = System.Enum.GetValues(typeof(CowActions));
        return (CowActions)actions.GetValue(Random.Range(0, actions.Length));
    }
}
