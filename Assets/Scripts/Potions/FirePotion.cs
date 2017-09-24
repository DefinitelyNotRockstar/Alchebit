using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : Potion {

    public float explosionDamage;
    public float explosionPower;
    public float explosionRadius;
	public GameObject explosionPrefab;
    public GameObject firePrefab;

	override public bool OnActivation(Collision2D collision) {

		GameObject fire = Instantiate(firePrefab);
        fire.transform.position = collision.contacts[0].point;

        Vector3 explosionPos = collision.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosionRadius);


		GameObject explosionInstance = Instantiate(explosionPrefab);
		explosionInstance.transform.position = collision.contacts[0].point;
        explosionInstance.GetComponentInChildren<ExplosionAnimator>().type = POTION.RIGHT;

            foreach (Collider2D hit in colliders) {
                if (hit.gameObject.CompareTag("Enemy")) {

                    Rigidbody2D rb = hit.gameObject.GetComponent<Rigidbody2D>();

                    if (rb != null) {
                        if (hit == collision.collider) {
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
