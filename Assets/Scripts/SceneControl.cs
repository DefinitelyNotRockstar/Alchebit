using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

    public GameObject enemy;


    void Update() {
        if (enemy == null) {
            SceneManager.LoadScene("Main");
        }
    }
}
