using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour {

    private float poisonRadius;

    private void Start() {
        poisonRadius = 0.3f;
    }


    private void FixedUpdate() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, poisonRadius);
        foreach (Collider2D hit in colliders) {
            if(hit.gameObject.CompareTag("Enemy")){
                hit.gameObject.GetComponent<Enemy>().ApplyDamage(FindObjectOfType<PoisonPotion>().potionDamage, POTION.DOWN);
            }
        }
    }
}
