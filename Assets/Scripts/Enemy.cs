using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float damage;

    private Player player;
    private DropsGenerator dropsGenerator;

    private void Awake() {
        player = FindObjectOfType<Player>();
        dropsGenerator = FindObjectOfType<DropsGenerator>();
    }


    public void ApplyDamage(float damageReceived, POTION sourceType) {
        health -= damageReceived;
        if (health <= 0) {
            dropsGenerator.DropFromEnemy(transform.position, sourceType);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Player") {
            player.ApplyDamage(damage, transform.position);
        }
    }

}
