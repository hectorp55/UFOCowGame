using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CattleController : MonoBehaviour
{
    public float safeBeamDistanceFar = 10f;
    public float safeBeamDistanceClose = 5f;
    public float maxSpeed = 3f;
    public Cow cow;
    AudioSource mooAudioSource;

    private float cycleTimer;
    private float waitDuration;
    private float actionDuration;
    private CowActions cowAction;
    private bool isGrounded;
    private const float walkSpeed = 0.5f;

    protected GameObject ufoBeam;

    protected const float runSpeed = 1f;

    abstract protected void DoWhenBeamIsTooClose();

    abstract protected void DoWhenBeamIsTooFar();

    void Awake() {
        int currentLevel = GameManager.GetManager().MissionCount;
        Range waitDurationRange = Difficulty.CowWaitTime(currentLevel);

        waitDuration = Random.Range(waitDurationRange.Min, waitDurationRange.Max);
        actionDuration = Random.Range(1f, 2f);
        cowAction = GetRandomCowAction();
        isGrounded = true;
        maxSpeed = Difficulty.CowSpeed(currentLevel);
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
                DoWhenBeamIsTooClose();
                return;
            }
            if (IsBeamToFar()) {
                DoWhenBeamIsTooFar();
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
                    return;
                    case CowActions.WalkLeft:
                    WalkLeft();
                    return;
                    case CowActions.WalkRight:
                    WalkRight();
                    return;
                }
            }

            //Set Animation Variables
            GetComponent<Animator>().SetFloat(AnimatorConstants.CowSpeed, 0);
        }
    }

    protected void Walk(Vector3 direction, float speed) {
        //Set Animation Variables
        GetComponent<Animator>().SetFloat(AnimatorConstants.CowSpeed, speed);

        //Flip sprite based on direction
        var isFacingLeft = direction.x < 0;
        GetComponent<SpriteRenderer>().flipX = isFacingLeft;

        Vector3 movement = direction * Time.deltaTime * speed * maxSpeed;
        transform.Translate(movement);

        if (transform.position.x <= ScreenConstants.LeftBound) {
            transform.position = new Vector3(ScreenConstants.RightBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= ScreenConstants.RightBound) {
            transform.position = new Vector3(ScreenConstants.LeftBound, transform.position.y, transform.position.z);
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
        Walk(Vector3.left, walkSpeed);
    }

    private void WalkRight() {
        Walk(Vector3.right, walkSpeed);
    }

    private void EatGrass() {
        // Trigger Eat Grass Animation
    }

    private CowActions GetRandomCowAction() {
        System.Array actions = System.Enum.GetValues(typeof(CowActions));
        return (CowActions)actions.GetValue(Random.Range(0, actions.Length));
    }
}
