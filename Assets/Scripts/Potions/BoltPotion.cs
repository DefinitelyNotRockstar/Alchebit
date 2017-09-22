using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPotion : Potion {

	public float potionSpeed;

	override public GameObject InstantiatePotion(Vector2 position, Vector2 direction) {
		GameObject potion = Instantiate(potionPrefab);
		potion.transform.position = position;
		PotionInstance potionInstance = potion.GetComponent<PotionInstance>();
		potionInstance.direction = direction;
		potionInstance.speed = potionSpeed;


		return potion;
	}
}
