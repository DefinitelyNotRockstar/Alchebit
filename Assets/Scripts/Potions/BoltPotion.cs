using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPotion : Potion {
    override public bool OnActivation(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            if (collision.gameObject.CompareTag("Enemy"))
                collision.gameObject.GetComponent<Enemy>().ApplyDamage(potionDamage);
            return true;
        }
        return false;
    }
}
