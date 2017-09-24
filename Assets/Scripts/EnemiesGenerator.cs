using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour {

    public GameObject enemyPrefab;
    public float timeBetweenWaves;
    public float timeWhenNoEnemies;
    public int initialEnemies;
    public int enemiesAddedEachWave;
    public Vector2 mapLimits;

    private Transform playerTransform;
    private int enemiesToSpawn;
    private float startTime;
    public int enemiesLeftAlive;


    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start() {
        createEnemies();
    }

    private void Update() {
        if (enemiesLeftAlive == 0) {
            startTime = Time.time;
        }
        if (Time.time - startTime > timeBetweenWaves || (Time.time - startTime > timeWhenNoEnemies && enemiesLeftAlive == 0)) {
            initialEnemies += enemiesAddedEachWave;
            createEnemies();
        }
    }

    private void createEnemies() {
        Vector2 enemyPosition;
        startTime = Time.time;
        enemiesToSpawn = initialEnemies;

        while (enemiesToSpawn > 0) {

            do {
                enemyPosition = new Vector2(Random.Range(-mapLimits.x, mapLimits.x), Random.Range(-mapLimits.y, mapLimits.y));
            } while (Vector2.Distance(enemyPosition, playerTransform.position) < 3.0f);

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = enemyPosition;

            enemiesToSpawn--;
        }
        enemiesLeftAlive = initialEnemies;
    }

    public void ReportEnemyDeath() {
        enemiesLeftAlive--;
    }


}
