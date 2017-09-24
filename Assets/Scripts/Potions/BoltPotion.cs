using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPotion : Potion {

    public GameObject explosionPrefab;
    public GameObject boltPrefab;

    override public bool OnActivation(Collider2D other, Vector2 collisionPos) {
        if (other.gameObject.CompareTag("Player"))
            return false;

        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<Enemy>().ApplyDamage(potionDamage, POTION.UP);
        
        GameObject explosionInstance = Instantiate(explosionPrefab);
        explosionInstance.transform.position = collisionPos;

        explosionInstance.GetComponentInChildren<ExplosionAnimator>().type = POTION.UP;

        return true;
    }
}
