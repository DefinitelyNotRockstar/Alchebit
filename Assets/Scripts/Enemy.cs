using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY {
    LEVEL0 = 0,
    LEVEL1 = 1,
    LEVEL2 = 2,
    LEVEL3 = 3,
    LEVEL4 = 4
}


public class Enemy : MonoBehaviour {

    public float health;
    public float damage;
    public int reward;
    public ENEMY type;
    public GameObject spawnPrefab;
    public GameObject deathPrefab;

    private Player player;
    private DropsGenerator dropsGenerator;
    private Score score;
    private EnemiesGenerator enemiesGenerator;


    private void Awake() {
        player = FindObjectOfType<Player>();
        dropsGenerator = FindObjectOfType<DropsGenerator>();
        score = FindObjectOfType<Score>();
        enemiesGenerator = FindObjectOfType<EnemiesGenerator>();
    }

    private void Start() {
        GameObject spawnInstance = Instantiate(spawnPrefab);
        spawnInstance.transform.position = transform.position;
    }


    public void ApplyDamage(float damageReceived, POTION sourceType) {
        health -= damageReceived;
        if (health <= 0) {
            dropsGenerator.DropFromEnemy(transform.position, sourceType, type);
            score.AddScore(reward);
            enemiesGenerator.ReportEnemyDeath();

            GameObject deathInstance = Instantiate(deathPrefab);
            deathInstance.transform.position = transform.position;

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Player") {
            player.ApplyDamage(damage, transform.position);
        }
    }

}
