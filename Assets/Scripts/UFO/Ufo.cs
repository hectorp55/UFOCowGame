using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject beam;
    public GameManager manager;
    public Cow lastCapturedCow;

    private Animator animator;

    void Start()
    {
        manager = GameManager.GetManager();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (beam.activeSelf) {
            CaptureCow(other.gameObject);
        }
    }

    public void Hover(float horizontalInput) {
        //Update Animator Variables
        animator.SetFloat(AnimatorConstants.InputDirection, horizontalInput);

        Vector3 movement = Vector3.right * Time.deltaTime * horizontalInput * moveSpeed;
        transform.Translate(movement);

        if (transform.position.x <= ScreenConstants.LeftBound) {
            transform.position = new Vector3(ScreenConstants.LeftBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= ScreenConstants.RightBound) {
            transform.position = new Vector3(ScreenConstants.RightBound, transform.position.y, transform.position.z);
        }
    }

    public void BeamOn() {
        beam.SetActive(true);
    }

    public void BeamOff() {
        beam.SetActive(false);
    }

    public void CaptureCow(GameObject cow) {
        if (cow.CompareTag(TagConstants.Cow)) {
            manager.CaptureCow(cow);
        }
    }

    public void Throwback() {
        Vector3 throwbackSpot = new Vector3(transform.position.x, 6.5f, 0); 
        manager.ThrowBackCow(throwbackSpot);
    }

    public void FlyHome() {
        animator.SetTrigger(AnimatorConstants.GoHome);
    }

    public void CompleteMission() {
        GameManager.GetManager().CompleteMission();
    }
}
