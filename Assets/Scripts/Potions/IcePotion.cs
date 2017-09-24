using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePotion : Potion {


    public GameObject icePrefab;
    public GameObject explosionPrefab;

    override public bool OnActivation(Collider2D other, Vector2 collisionPos) {
		if (other.gameObject.CompareTag("Player"))
			return false;

        GameObject poison = Instantiate(icePrefab);
        poison.transform.position = collisionPos;
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().ApplyDamage(potionDamage, POTION.LEFT);
        }

        GameObject explosionInstance = Instantiate(explosionPrefab);
        explosionInstance.transform.position = collisionPos;

        explosionInstance.GetComponentInChildren<ExplosionAnimator>().type = POTION.LEFT;

        return true;

    }

}
