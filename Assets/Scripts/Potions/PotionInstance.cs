using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInstance : MonoBehaviour {

    public delegate bool ActivatePotion(Collider2D other, Vector2 collisionPos);
    public ActivatePotion _potionFunction;


    public float speed;
    public float damage;

	private Vector2 direction;

    public float explodeVelocityThresh = 0.1f;

	private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        direction = Vector2.zero;
    }

    private void Update() {

        if(rigidBody.velocity.magnitude < explodeVelocityThresh){

            if(_potionFunction(GetComponent<Collider2D>(), transform.position)){
                Destroy(gameObject);
            }

        }

        Move();
    }

    public void StartMoving(Vector2 o_direction){
        direction = o_direction;
        rigidBody.velocity = new Vector2(explodeVelocityThresh, explodeVelocityThresh);
        rigidBody.AddForce(direction.normalized * speed , ForceMode2D.Force);
    }

    private void Move() {
        //transform.position =  (Vector2) transform.position + direction * speed;
        //potionRigidbody.MovePosition((Vector2) transform.position + direction * speed);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (_potionFunction(collision.collider, collision.contacts[0].point)) {
            Destroy(gameObject);
        }
    }
}
