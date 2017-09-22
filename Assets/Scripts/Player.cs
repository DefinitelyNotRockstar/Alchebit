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
    private static Vector2[] directions = {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private void Awake() {
        potions = new Potion[4];
        potions[(int) POTION.UP] = FindObjectOfType<BoltPotion>();
        potions[(int) POTION.DOWN] = FindObjectOfType<PoisonPotion>();
        potions[(int) POTION.LEFT] = FindObjectOfType<IcePotion>();
        potions[(int) POTION.RIGHT] = FindObjectOfType<FirePotion>();
    }


    public void ThrowPotion(POTION type) {
        potions[(int) type].InstantiatePotion(transform.position, directions[(int) type]);
    }

}
