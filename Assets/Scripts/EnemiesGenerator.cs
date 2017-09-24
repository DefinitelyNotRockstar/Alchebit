using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Level {
    public int[] enemies;
    public Level(int[] enem) {
        enemies = enem;
    }
}


public class EnemiesGenerator : MonoBehaviour {

    public GameObject enemyPrefab;
    public float timeBetweenWaves;
    public float timeWhenNoEnemies;
    public int initialEnemies;
    public float minTimeBetweenSpawns = 0.25f;
    public float maxTimeBetweenSpawns = 0.5f;
    public Vector2 mapLimits;

    private Transform playerTransform;
    private AudioSource audioSource;
    private int enemiesToSpawn;
    private float startTime;
    public int enemiesLeftAlive;
    public int currentLevel;


    private Level[] levels = new Level[] {
        new Level(new int[] {
            5,0,0,0,0
        }),
        new Level(new int[] {
            5,2,0,0,0
        }),
        new Level(new int[] {
            4,4,1,0,0
        }),
        new Level(new int[] {
            3,3,3,0,0
        }),
        new Level(new int[] {
            0,5,5,0,0
        }),
        new Level(new int[] {
            0,3,6,1,0
        }),
        new Level(new int[] {
            0,0,7,3,0
        }),
        new Level(new int[] {
            0,0,5,5,0
        }),
        new Level(new int[] {
            0,0,2,6,1
        }),
        new Level(new int[] {
            0,0,0,7,2
        }),
        new Level(new int[] {
            0,0,0,5,4
        }),
        new Level(new int[] {
            0,0,0,3,7
        }),
        new Level(new int[] {
            0,0,0,0,10
        }),
    };



    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        //currentLevel = 0;
        enemiesLeftAlive = 0;
    }

    private void Update() {

        if (Time.time - startTime > timeBetweenWaves || enemiesLeftAlive == 0) {
            StartCoroutine("CreateEnemiesCoroutine");

        }
    }

    private void CreateEnemies() {
        Vector2 enemyPosition;
        startTime = Time.time;
        enemiesToSpawn = initialEnemies;

        while (enemiesToSpawn > 0) {

            do {
                enemyPosition = new Vector2(Random.Range(-mapLimits.x, mapLimits.x), Random.Range(-mapLimits.y, mapLimits.y));
            } while (Vector2.Distance(enemyPosition, playerTransform.position) < 3.0f);

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<Enemy>().type = (ENEMY) Random.Range(0f, currentLevel);
            //enemy.GetComponent<EnemyController>().UpdateAnimations();
            enemy.transform.position = enemyPosition;

            enemiesToSpawn--;
        }
        enemiesLeftAlive = initialEnemies;
    }

    public void ReportEnemyDeath() {
        enemiesLeftAlive--;
    }

    private IEnumerator CreateEnemiesCoroutine() {

        Vector2 enemyPosition;
        startTime = Time.time;


        int level = currentLevel;
        if (currentLevel < levels.Length - 1)
            currentLevel++;
        enemiesLeftAlive = 0;
        for (int i = 0; i < levels[level].enemies.Length; i++) {
            enemiesLeftAlive += levels[level].enemies[i];
            for (int j = 0; j < levels[level].enemies[i]; j++) {
                audioSource.Play();
                do {
                    enemyPosition = new Vector2(Random.Range(-mapLimits.x, mapLimits.x), Random.Range(-mapLimits.y, mapLimits.y));
                } while (Vector2.Distance(enemyPosition, playerTransform.position) < 3.0f);

                GameObject enemy = Instantiate(enemyPrefab);
                enemy.transform.position = enemyPosition;
                enemy.GetComponent<Enemy>().type = (ENEMY) i;
                enemy.GetComponent<Enemy>().health *= i + 1;
                enemy.GetComponent<EnemyController>().speed += i * 3;

                yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
            }
        }



    }

}
