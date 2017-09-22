using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Player player;
    private Rigidbody2D playerRigidBody;
    private Vector2 movement;

    private void Awake() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Update() {
		Shoot();
    }

    private void FixedUpdate() {
        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");
        Move(lh, lv);
	}

	private void Move(float lh, float lv) {
		movement.Set(lh, lv);
		movement = movement.normalized * speed / 10;
        playerRigidBody.MovePosition((Vector2)transform.position + movement);
	}

    private void Shoot() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            player.ThrowPotion(POTION.UP);
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
			player.ThrowPotion(POTION.DOWN);
			return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            player.ThrowPotion(POTION.LEFT);
			return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            player.ThrowPotion(POTION.RIGHT);
			return;
        }
    }
}
