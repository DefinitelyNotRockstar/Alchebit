using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Potion {

    public GameObject poisonPrefab;
    public GameObject explosionPrefab;

    override public bool OnActivation(Collider2D other, Vector2 collisionPos) {
        if (other.gameObject.CompareTag("Player"))
            return false;

        GameObject poison = Instantiate(poisonPrefab);
        poison.transform.position = collisionPos;

        GameObject explosionInstance = Instantiate(explosionPrefab);
        explosionInstance.transform.position = collisionPos;
        explosionInstance.GetComponentInChildren<ExplosionAnimator>().type = POTION.DOWN;


        return true;

    }

}
