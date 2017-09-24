using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float movementThreashold = 0.20f;


    private Player player;
    private Rigidbody2D playerRigidBody;
    public float potionCooldown;
    private float lastPotion;

    //Animation
    private Animator animator;
    private float lastXDirection;

    //Movement
    private Vector2 movement;
    private float remainingRestrictionTime;

    //Sound
    private AudioSource audioSource;
    public AudioClip throwClip;
    public AudioClip takeDamageClip;
    public AudioClip pickupClip;



    public void RestrictMovement(float time) {
        remainingRestrictionTime = time;
    }

    public void AnimateDamage() {

        animator.Play("TakingDamage");

    }


    private void Awake() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        remainingRestrictionTime = 0.0f;
    }

    private void Update() {
        if (Time.time - lastPotion > potionCooldown)
            Shoot();
        UpdateAnimations();
    }

    private void FixedUpdate() {
        if (remainingRestrictionTime > 0.0f) {
            remainingRestrictionTime -= Time.fixedDeltaTime;
            if (remainingRestrictionTime < 0.0f) {
                playerRigidBody.velocity = Vector2.zero;
            }
        } else {
            float lh = Input.GetAxisRaw("Horizontal");
            float lv = Input.GetAxisRaw("Vertical");
            Move(lh, lv);
        }
    }

    private void Move(float lh, float lv) {
        movement.Set(lh, lv);
        if (movement.magnitude > movementThreashold) {
            lastXDirection = movement.normalized.x;
            movement = movement.normalized * speed / 10;
            playerRigidBody.velocity = movement;
        } else {
            movement = Vector2.zero;
        }
    }

    private void Shoot() {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButton("3")) {
            animator.Play("ThrowingPotion");
            player.ThrowPotion(POTION.UP);
            lastPotion = Time.time;
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButton("0")) {
            animator.Play("ThrowingPotion");
            player.ThrowPotion(POTION.DOWN);
            lastPotion = Time.time;
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButton("2")) {
            animator.Play("ThrowingPotion");
            player.ThrowPotion(POTION.LEFT);
            lastPotion = Time.time;
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButton("1")) {
            animator.Play("ThrowingPotion");
            player.ThrowPotion(POTION.RIGHT);
            lastPotion = Time.time;
            return;
        }
    }

    private void UpdateAnimations() {
        animator.SetBool("IsMoving", (this.movement.magnitude > 0.0f));
        animator.SetFloat("XSpeed", movement.normalized.x);
        animator.SetFloat("XLastDirection", lastXDirection);
    }

    public void PlaySound(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
