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
    public int maxAmmo;

    private readonly Vector2[] directions = {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private Rigidbody2D playerRigidbody;
    private float[] ammo;

    private void Awake() {
        potions = new Potion[4];
        potions[(int) POTION.UP] = FindObjectOfType<BoltPotion>();
        potions[(int) POTION.DOWN] = FindObjectOfType<PoisonPotion>();
        potions[(int) POTION.LEFT] = FindObjectOfType<IcePotion>();
        potions[(int) POTION.RIGHT] = FindObjectOfType<FirePotion>();

        ammo = new float[4] { maxAmmo, maxAmmo, maxAmmo, maxAmmo };

        playerRigidbody = GetComponent<Rigidbody2D>();
    }



    public void ThrowPotion(POTION type) {
        if (ammo[(int) type] >= 1f) {
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

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "CollectableBoltPotion":
                if (ammo[(int) POTION.UP] < maxAmmo) {
                    ammo[(int) POTION.UP] += 0.25f;
                    Destroy(other.gameObject);
                }
                break;
            case "CollectablePoisonPotion":
                if (ammo[(int) POTION.DOWN] < maxAmmo) {
                    ammo[(int) POTION.DOWN] += 0.25f;
                    Destroy(other.gameObject);
                }
                break;
            case "CollectableIcePotion":
                if (ammo[(int) POTION.LEFT] < maxAmmo) {
                    ammo[(int) POTION.LEFT] += 0.25f;
                    Destroy(other.gameObject);
                }
                break;
            case "CollectableFirePotion":
                if (ammo[(int) POTION.RIGHT] < maxAmmo) {
                    ammo[(int) POTION.RIGHT] += 0.25f;
                    Destroy(other.gameObject);
                }
                break;
        }
    }

}
