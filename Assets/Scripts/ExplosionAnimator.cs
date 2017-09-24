using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimator : MonoBehaviour {



    public POTION type = POTION.RIGHT;

    private Animator animator;

    private void Awake()
    {
		animator = GetComponent<Animator>();
	}

    // Use this for initialization
    void Start () {
        animator.SetFloat("Type",(float)type);
        animator.Play("Explosion");
        Destroy(transform.parent.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
