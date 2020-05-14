using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public bool correctCow;
    AudioSource mooAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        mooAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Moo()
    {
        mooAudioSource.Play(0);
    }
}
