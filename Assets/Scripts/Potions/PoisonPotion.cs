using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Potion {

    public GameObject poisonPrefab;
    public GameObject explosionPrefab;

    override public bool OnActivation(Collision2D collision) {




        GameObject poison = Instantiate(poisonPrefab);
		poison.transform.position = collision.contacts[0].point;

        GameObject explosionInstance = Instantiate(explosionPrefab);
		explosionInstance.transform.position = collision.contacts[0].point;
		explosionInstance.GetComponentInChildren<ExplosionAnimator>().type = POTION.DOWN;

            
        return true;
       
    }

}
