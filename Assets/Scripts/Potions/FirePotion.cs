using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : Potion {

    public float explosionDamage;
    public float explosionPower;
    public float explosionRadius;
    public GameObject explosionPrefab;
    public GameObject firePrefab;

    override public bool OnActivation(Collider2D other, Vector2 collisionPos) {
		if (other.gameObject.CompareTag("Player"))
			return false;

        GameObject fire = Instantiate(firePrefab);
        fire.transform.position = collisionPos;

        Vector3 explosionPos = collisionPos;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosionRadius);


        GameObject explosionInstance = Instantiate(explosionPrefab);
        explosionInstance.transform.position = collisionPos;
        explosionInstance.GetComponentInChildren<ExplosionAnimator>().type = POTION.RIGHT;

        foreach (Collider2D hit in colliders) {
            if (hit.gameObject.CompareTag("Enemy")) {

                Rigidbody2D rb = hit.gameObject.GetComponent<Rigidbody2D>();

                if (rb != null) {
                    if (hit == other) {
                        rb.AddForce(direction * explosionPower, ForceMode2D.Force);
                    } else {
                        rb.AddForce((hit.transform.position - explosionPos).normalized * explosionPower, ForceMode2D.Force);
                    }
                    hit.gameObject.GetComponent<Enemy>().ApplyDamage(explosionDamage, POTION.RIGHT);
                }
            }
        }
        return true;
    }

}
