using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    public float movementDelay;

    private float _speed;
    private float _slowSpeed;
    private Vector2 movement;
    private float startTime;

    //Animation
    private Animator animator;
    private float lastXDirection;


    private Transform playerTransform;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        _speed = speed;
        _slowSpeed = speed / 2;
        startTime = Time.time;
    }

    private void Update() {
        UpdateAnimations();
    }

    private void FixedUpdate() {

        if (Time.time - startTime > movementDelay) {
            Vector2 vectorToPlayer = playerTransform.position - this.transform.position;
            movement = vectorToPlayer.normalized;
            lastXDirection = movement.x;
            transform.position += ((Vector3) vectorToPlayer).normalized * speed / 500;
        }
    }

    public void Slow() {
        speed = _slowSpeed;
    }

    public void EndSlow() {
        speed = _speed;
    }


    public void UpdateAnimations() {
        animator.SetFloat("Type", (float) GetComponent<Enemy>().type);
        animator.SetBool("IsMoving", (this.movement.magnitude > 0.0f));
        animator.SetFloat("XSpeed", movement.normalized.x);
        animator.SetFloat("XLastDirection", lastXDirection);
    }

}
