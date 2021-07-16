using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public Text coinText;
    public GameObject scoreManager;
    public ScoreManager score;

    private void Start() {
        coinText = GetComponent<Text>();
        scoreManager = GameObject.Find("ScoreManager");
        score = scoreManager.GetComponent<ScoreManager>();
    }

    private void Update() {
        if(score.coins <= 9 ){
            coinText.text = "0"+ score.coins.ToString();
        }else{
            coinText.text = score.coins.ToString();
        }
        
    }

}
