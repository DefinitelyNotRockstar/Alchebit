using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

    public GameObject enemy;
    public AudioSource audioSource;

    void Update() {
        if (enemy == null) {
            AudioControl.Time = audioSource.time;
            SceneManager.LoadScene("Main");
        }
    }
}
