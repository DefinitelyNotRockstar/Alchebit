using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour{


    public GameObject potionPrefab;

    public virtual GameObject InstantiatePotion(Vector2 position, Vector2 direction) {
        return Instantiate(potionPrefab);
    }


}
