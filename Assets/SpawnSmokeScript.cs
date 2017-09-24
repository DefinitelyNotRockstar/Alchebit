using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSmokeScript : MonoBehaviour {


	private Animator animator;
	private AudioSource audioSource;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start()
	{
		animator.Play("Spawn_Smoke");
		audioSource.Play();
		Destroy(transform.parent.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

	}

	// Update is called once per frame
	void Update()
	{

	}
}
