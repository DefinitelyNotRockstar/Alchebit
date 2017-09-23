using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Potion {

    public GameObject poisonPrefab;

    override public bool OnActivation(Collision2D collision) {
        
        if (!collision.gameObject.CompareTag("Player")) {
            if (collision.gameObject.CompareTag("Enemy")) {
                GameObject poison = Instantiate(poisonPrefab);
                poison.transform.position = collision.collider.transform.position;


            }
            return true;
        }
        return false;
    }

}
