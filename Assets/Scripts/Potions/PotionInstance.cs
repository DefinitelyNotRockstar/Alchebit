using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInstance : MonoBehaviour {

    public Vector2 direction;
    public float speed;

    private Rigidbody2D potionRigidbody;

    private void Awake() {
        direction = Vector2.zero;
        potionRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        potionRigidbody.MovePosition((Vector2) transform.position + direction * speed);
    }
}
