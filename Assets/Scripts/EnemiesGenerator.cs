using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour {

    public GameObject enemyPrefab;
    public float timeBetweenWaves;
    public int initialEnemies;

    private Transform playerTransform;
    private int enemiesLeft;
    private float startTime;


    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start() {
        createEnemies();
    }

    private void Update() {
        if (Time.time - startTime > timeBetweenWaves){
                     
			createEnemies();
        }
    }

    private void createEnemies() {
        Vector2 enemyPosition;
        startTime = Time.time;
        enemiesLeft = initialEnemies;

        while (enemiesLeft > 0) {

            do {
                enemyPosition = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));
            } while (Vector2.Distance(enemyPosition, playerTransform.position) < 3.0f);

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = enemyPosition;

            enemiesLeft--;
        }
    }


}
