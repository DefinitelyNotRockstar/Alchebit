using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimator : MonoBehaviour {



    public POTION type = POTION.RIGHT;
    public AudioClip[] explosionClips = new AudioClip[4];

    private Animator animator;
    private AudioSource audioSource;

    private void Awake() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start() {
        animator.SetFloat("Type", (float) type);
        animator.Play("Explosion");
        audioSource.clip = explosionClips[(int) type];
        audioSource.Play();
        Destroy(transform.parent.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update() {

    }
}
