using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePotion : Potion {


    public GameObject icePrefab;

	override public bool OnActivation(Collision2D collision) {

		if (!collision.gameObject.CompareTag("Player")) {
			if (collision.gameObject.CompareTag("Enemy")) {
				GameObject poison = Instantiate(icePrefab);
				poison.transform.position = collision.collider.transform.position;
                collision.gameObject.GetComponent<Enemy>().ApplyDamage(potionDamage, POTION.LEFT);
			}
			return true;
		}
		return false;
	}

}
