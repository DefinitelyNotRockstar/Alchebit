using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPotion : Potion {
    override public bool OnActivation(Collider2D other, Vector2 collisionPos) {

        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<Enemy>().ApplyDamage(potionDamage, POTION.UP);
        return true;
    }
}
