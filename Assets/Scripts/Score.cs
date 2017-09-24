using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text scoreText;
    public float totalUpdatingTime;
    public float minTimeChar;
    public float maxTimeChar;

    private int score;
    private bool updatingScore;
    private int initialScore;

    private void Start() {
        score = 0;
        updatingScore = false;
    }


    private void Update() {
        DisplayScore();
    }

    public void AddScore(int points) {
        initialScore = score;
        score += points;
        if (scoreText != null)
            scoreText.gameObject.GetComponent<Animator>().Play("ScoreAnimation");
    }


    private void DisplayScore() {
        scoreText.text = score.ToString().PadLeft(scoreText.text.Length, '0');
    }

}
