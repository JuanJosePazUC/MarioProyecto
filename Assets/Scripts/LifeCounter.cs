using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    public Text lifeText;
    public GameObject ScoreManager;
    public ScoreManager score;

    private void Start() {
        lifeText = GetComponent<Text>();
        ScoreManager = GameObject.Find("ScoreManager");
        score = ScoreManager.GetComponent<ScoreManager>();
    }

    private void Update() {
        lifeText.text = score.vidas.ToString();   
    }
}
