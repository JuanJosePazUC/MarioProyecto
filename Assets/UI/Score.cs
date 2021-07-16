using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public GameObject ScoreManager;
    public ScoreManager score;

    private void Start() {
        scoreText = GetComponent<Text>();
        ScoreManager = GameObject.Find("ScoreManager");
        score = ScoreManager.GetComponent<ScoreManager>();
    }

    private void Update() {
        scoreText.text = score.score.ToString();   
    }
}
