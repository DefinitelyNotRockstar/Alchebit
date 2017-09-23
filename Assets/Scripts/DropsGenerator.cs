using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsGenerator : MonoBehaviour {

    public float randomDropCooldown;
    public int maxDropFromEnemy = 1;
    public int minDropFromEnemy = 4;
    public GameObject[] collectablePotions = new GameObject[4];

    private float lastDropTime;

    private void Start() {
        lastDropTime = Time.time;
    }

    private void Update() {
        if (Time.time - lastDropTime > randomDropCooldown) {
            RandomDrop();
        }
    }

    private void RandomDrop() {
        Vector2 position = new Vector2(Random.Range(-15, 15), Random.Range(-15, 15));
        POTION type = (POTION) Random.Range(0, 4);
        GameObject collectablePotion = Instantiate(collectablePotions[(int) type]);
        collectablePotion.transform.position = position;
        lastDropTime = Time.time;
    }

    public void DropFromEnemy(Vector2 position, POTION excludedType) {
        int nDrops = Random.Range(minDropFromEnemy, maxDropFromEnemy);
        POTION type;
        Vector2 posOffset;
        for (int i = 0; i < nDrops; i++) {
            do {
                type = (POTION) Random.Range(0, 4);
            } while (type == excludedType);

            posOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            Debug.Log("Dropping");

			GameObject collectablePotion = Instantiate(collectablePotions[(int) type]);
			collectablePotion.transform.position = position + posOffset;
        }
    }

}
