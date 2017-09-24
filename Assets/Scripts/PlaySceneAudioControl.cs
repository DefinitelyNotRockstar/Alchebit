using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneAudioControl : MonoBehaviour {

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        audioSource.time = AudioControl.Time;
        audioSource.Play();
        audioSource.time = AudioControl.Time;
    }

}
