using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POTION {
    UP = 0,
    DOWN = 1,
    LEFT = 2,
    RIGHT = 3
}

public class Player : MonoBehaviour {

    public Potion[] potions;
    public float health;

    private readonly Vector2[] directions = {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private Rigidbody2D playerRigidbody;
    private int[] ammo;

    private void Awake() {
        potions = new Potion[4];
        potions[(int) POTION.UP] = FindObjectOfType<BoltPotion>();
        potions[(int) POTION.DOWN] = FindObjectOfType<PoisonPotion>();
        potions[(int) POTION.LEFT] = FindObjectOfType<IcePotion>();
        potions[(int) POTION.RIGHT] = FindObjectOfType<FirePotion>();

        ammo = new int[4] { 15, 15, 15, 15 };

        playerRigidbody = GetComponent<Rigidbody2D>();
    }



    public void ThrowPotion(POTION type) {
        if (ammo[(int) type] > 0) {
            potions[(int) type].InstantiatePotion(transform.position, directions[(int) type]);
            ammo[(int) type]--;
        }
    }

    public void ApplyDamage(float damageReceived, Vector2 enemyPosition) {
        health -= damageReceived;
        playerRigidbody.AddForce(((Vector2) transform.position - enemyPosition).normalized * 500, ForceMode2D.Force);

        if (health <= 0) {
            Debug.Log("YOU DIED");
        }
    }

}
