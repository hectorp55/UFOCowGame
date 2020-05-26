using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Cow cow;
    AudioSource mooAudioSource;

    private float cycleTimer;
    private float waitDuration;
    private float actionDuration;
    private CowActions cowAction;
    private bool isGrounded;

    void Awake() {
        waitDuration = Random.Range(2f, 5f);
        actionDuration = Random.Range(1f, 2f);

        cowAction = GetRandomCowAction();

        isGrounded = true;
    }

    void Start() {
        mooAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        // If cow is on the ground
        if (isGrounded) {
            cycleTimer += Time.deltaTime;

            if(cycleTimer > waitDuration + actionDuration) {
            // Restart Cycle
            cycleTimer = 0;
            cowAction = GetRandomCowAction();
            } 
            // else if beam is on
                // RunAwayFromBeam()
            else if (cycleTimer > waitDuration && cycleTimer < waitDuration + actionDuration) {
                // Do Random Cow Action
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
        // Walk(Vector3 awayFromBeam);
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
