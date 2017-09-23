using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {


    public GameObject potionPrefab;
    public float potionSpeed;
    public float potionDamage;

    protected Vector2 direction;

    public GameObject InstantiatePotion(Vector2 position, Vector2 direction) {
		GameObject potion = Instantiate(potionPrefab);
		potion.transform.position = position;
		PotionInstance potionInstance = potion.GetComponent<PotionInstance>();
		potionInstance.direction = direction;
        this.direction = direction;
		potionInstance.speed = potionSpeed;
        potionInstance.damage = potionDamage;
        potionInstance._potionFunction = OnActivation;


		return potion;
    }

    public virtual bool OnActivation(Collision2D collision){
		GameObject collisionObject = collision.collider.gameObject;
		if (!collisionObject.CompareTag("Player")) {
			if (collisionObject.CompareTag("Enemy")) {
                collisionObject.GetComponent<Enemy>().ApplyDamage(potionDamage);
			}
            return true;
		}
        return false;
    }
}
