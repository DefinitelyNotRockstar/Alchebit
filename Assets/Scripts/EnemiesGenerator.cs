using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour {

    public GameObject enemyPrefab;
    public float timeBetweenWaves;
    public float timeWhenNoEnemies;
    public int initialEnemies;
    public int enemiesAddedEachWave;
    public float minTimeBetweenSpawns = 0.25f;
    public float maxTimeBetweenSpawns = 0.5f;
    public Vector2 mapLimits;

    private Transform playerTransform;
    private AudioSource audioSource;
    private int enemiesToSpawn;
    private float startTime;
    private int enemiesLeftAlive;
    private int currentLevel;


    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        currentLevel = 0;
        enemiesLeftAlive = 0;
    }

    private void Update() {

        if (Time.time - startTime > timeBetweenWaves || enemiesLeftAlive == 0) {
            StartCoroutine("CreateEnemiesCoroutine");
			initialEnemies += enemiesAddedEachWave;
            if (currentLevel < 4)
                currentLevel++;
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
            enemy.transform.position = enemyPosition;
            enemy.GetComponent<Enemy>().type = (ENEMY) Random.Range(0f, currentLevel);


            enemiesToSpawn--;
        }
        enemiesLeftAlive = initialEnemies;
    }

    public void ReportEnemyDeath() {
        enemiesLeftAlive--;
    }

    private IEnumerator CreateEnemiesCoroutine(){

		Vector2 enemyPosition;
		startTime = Time.time;
        enemiesToSpawn = initialEnemies;
		enemiesLeftAlive = initialEnemies;
        audioSource.Play();
        while (enemiesToSpawn > 0) {
            audioSource.Play();
            do {
                enemyPosition = new Vector2(Random.Range(-mapLimits.x, mapLimits.x), Random.Range(-mapLimits.y, mapLimits.y));
            } while (Vector2.Distance(enemyPosition, playerTransform.position) < 3.0f);

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = enemyPosition;
            enemy.GetComponent<Enemy>().type = (ENEMY) Random.Range(0f, currentLevel);


            enemiesToSpawn--;
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
        }

    }

}
