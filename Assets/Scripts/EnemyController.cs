using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    private float _speed;
    private float _slowSpeed;


    private Transform playerTransform;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start() {
        _speed = speed;
        _slowSpeed = speed / 2;
    }

    private void FixedUpdate() {

        this.transform.position += (playerTransform.position - this.transform.position).normalized * speed / 500;
    }

    public void Slow() {
        speed = _slowSpeed;
    }

    public void EndSlow(){
        speed = _speed;
    }
}
