using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoButtonsAnimator : MonoBehaviour {


    public Animator[] animators = new Animator[4];


    public void SetValue(POTION type, float value){

        animators[(int)type].SetFloat("Filled",value);

    }

	// Use this for initialization
	void Start () {

        for (short i = 0; i < 4; i++)
        {
            animators[i].SetFloat("Type", (float)i);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
