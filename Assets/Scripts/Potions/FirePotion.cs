using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotion : Potion {

    public float explosionDamage;
    public float explosionPower;
    public float explosionRadius;

    override public bool OnActivation(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            Vector3 explosionPos = collision.transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosionRadius);
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
        return false;
    }

}
