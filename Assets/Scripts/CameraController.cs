using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour {

    public float movementSmoothness;

    private Transform playerTransform;
    private Vector3 posOffset;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Assert.IsNotNull(playerTransform);
        posOffset = transform.position;
    }

    private void Update() {
        this.transform.position = Vector3.Lerp(this.transform.position, playerTransform.position + posOffset, movementSmoothness);
    }
}
