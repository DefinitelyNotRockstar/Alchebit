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
    public float hitDelay = 0.2f;
    public float DamageForce = 1000.0f;
    public float pickUpValue = 0.25f;
    public Transform potionOrigin;

    private readonly Vector2[] directions = {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private Rigidbody2D playerRigidbody;
    private PlayerController playerController;
    private AmmoButtonsAnimator ammoButtonsAnimator;


    private float[] ammo;

    private void Awake() {
        potions = new Potion[4];
        potions[(int) POTION.UP] = FindObjectOfType<BoltPotion>();
        potions[(int) POTION.DOWN] = FindObjectOfType<PoisonPotion>();
        potions[(int) POTION.LEFT] = FindObjectOfType<IcePotion>();
        potions[(int) POTION.RIGHT] = FindObjectOfType<FirePotion>();

        ammo = new float[4] { maxAmmo, maxAmmo, maxAmmo, maxAmmo };


        playerRigidbody = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        ammoButtonsAnimator = FindObjectOfType<AmmoButtonsAnimator>();

		for (short i = 0; i < 4; i++)
		{
            ammoButtonsAnimator.SetValue((POTION)i, Mathf.Floor(ammo[i]) / maxAmmo);
		}

    }



    public void ThrowPotion(POTION type) {
        if (ammo[(int) type] >= 1f) {
            potions[(int) type].InstantiatePotion(potionOrigin.position, directions[(int) type]);
            ammo[(int) type]--;
            ammoButtonsAnimator.SetValue(type, Mathf.Floor(ammo[(int)type]) / maxAmmo);
		}
    }

    public void ApplyDamage(float damageReceived, Vector2 enemyPosition) {
        health -= damageReceived;
        playerRigidbody.AddForce(((Vector2) transform.position - enemyPosition).normalized * DamageForce, ForceMode2D.Force);
        playerController.RestrictMovement(hitDelay);
        playerController.AnimateDamage();
        if (health <= 0) {
            Debug.Log("YOU DIED");
        }
    }

    private bool addAmmo(POTION type, float value){
        if (ammo[(int)type] < maxAmmo) {
            ammo[(int)type] += value;
            ammoButtonsAnimator.SetValue(type, Mathf.Floor(ammo[(int)type]) / maxAmmo);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "CollectableBoltPotion":
                if (addAmmo(POTION.UP,pickUpValue))
                {
					Destroy(other.gameObject);
                }
                break;
            case "CollectablePoisonPotion":
				if (addAmmo(POTION.DOWN, pickUpValue))
				{
					Destroy(other.gameObject);
				}
				break;
            case "CollectableIcePotion":
				if (addAmmo(POTION.LEFT, pickUpValue))
				{
					Destroy(other.gameObject);
				}
				break;
            case "CollectableFirePotion":
				if (addAmmo(POTION.RIGHT, pickUpValue))
				{
					Destroy(other.gameObject);
				}
				break;
        }
    }

}
