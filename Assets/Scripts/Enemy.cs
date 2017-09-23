using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    public int damage;

    private Player player;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    private void Start() {
    }

    public void ApplyDamage(int damageReceived) {
        health -= damageReceived;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Player") {
            player.ApplyDamage(damage);
        }
    }

}
