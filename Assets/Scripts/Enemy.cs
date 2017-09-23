using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public float damage;

    private Player player;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }


    public void ApplyDamage(float damageReceived) {
        health -= damageReceived;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Player") {
            player.ApplyDamage(damage, transform.position);
        }
    }

}
