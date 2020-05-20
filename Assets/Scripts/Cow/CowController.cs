using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public Cow cow;
    AudioSource mooAudioSource;

    void Start()
    {
        mooAudioSource = GetComponent<AudioSource>();
    }

    public void Moo()
    {
        mooAudioSource.Play(0);
    }
}
