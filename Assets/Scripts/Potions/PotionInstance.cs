using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInstance : MonoBehaviour {

    public delegate bool ActivatePotion(Collision2D collision);
    public ActivatePotion _potionFunction;


    public Vector2 direction;
    public float speed;
    public float damage;

    private void Awake() {
        direction = Vector2.zero;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        transform.position =  (Vector2) transform.position + direction * speed;
        //potionRigidbody.MovePosition((Vector2) transform.position + direction * speed);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (_potionFunction(collision)) {
            Destroy(gameObject);
        }
    }
}
