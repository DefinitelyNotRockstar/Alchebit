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
        this.direction = direction;
		potionInstance.speed = potionSpeed;
        potionInstance.damage = potionDamage;
        potionInstance._potionFunction = OnActivation;
        potionInstance.StartMoving(direction);

		return potion;
    }

    public virtual bool OnActivation(Collider2D other, Vector2 collisionPos){
		GameObject collisionObject = other.gameObject;
		if (!collisionObject.CompareTag("Player")) {
			if (collisionObject.CompareTag("Enemy")) {
                collisionObject.GetComponent<Enemy>().ApplyDamage(potionDamage, POTION.UP);
			}
            return true;
		}
        return false;
    }
}
