using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    private float _speed;
    private float _slowSpeed;
    private Vector2 movement;

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
    }

    private void Update() {
        UpdateAnimations();
    }

    private void FixedUpdate() {

        Vector2 vectorToPlayer = playerTransform.position - this.transform.position;
        movement = vectorToPlayer.normalized;
        lastXDirection = movement.x;
        transform.position += ((Vector3)vectorToPlayer).normalized * speed / 500;
    }

    public void Slow() {
        speed = _slowSpeed;
    }

    public void EndSlow(){
        speed = _speed;
    }


	private void UpdateAnimations()
	{

		animator.SetBool("IsMoving", (this.movement.magnitude > 0.0f));
		animator.SetFloat("XSpeed", movement.normalized.x);
		animator.SetFloat("XLastDirection", lastXDirection);

	}

}
