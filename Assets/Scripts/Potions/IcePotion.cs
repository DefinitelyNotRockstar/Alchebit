using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePotion : Potion {


    public GameObject icePrefab;
    public GameObject explosionPrefab;

	override public bool OnActivation(Collision2D collision) {

                GameObject poison = Instantiate(icePrefab);
                poison.transform.position = collision.contacts[0].point;
		if (collision.gameObject.CompareTag("Enemy")) {
                collision.gameObject.GetComponent<Enemy>().ApplyDamage(potionDamage, POTION.LEFT);
			}

		GameObject explosionInstance = Instantiate(explosionPrefab);
		explosionInstance.transform.position = collision.contacts[0].point;

		explosionInstance.GetComponentInChildren<ExplosionAnimator>().type = POTION.LEFT;

			return true;
		
	}

}
