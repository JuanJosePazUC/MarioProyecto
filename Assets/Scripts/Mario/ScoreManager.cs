using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int coins;
    public int score;
    public int vidas;
    public GameObject textScore;
    public float xOffSet = 1f;
    public float yOffSet = 0.5f;

    private static ScoreManager _instance;
    
    void Awake(){

        if (_instance == null){

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
    
        } else {
            Destroy(this);
        }
    }
    
    private void Start() {
        coins = 0;
        score = 0;
        vidas = 2;
    }

    public void ColectCoin(int coin){
        coins = coins + coin;
        if(coins >= 100){
            coins = coins - 100;
            LifeUp(1);
        }
    }

    public void LifeUp(int life){
        vidas = vidas + life;
    }

    public void ScoreUp(int aScore, Transform transformHitted){
        textScore.GetComponentInChildren<TextMesh>().text = aScore.ToString("0");
        Instantiate(textScore, new Vector3(transformHitted.position.x + xOffSet, transformHitted.position.y + yOffSet), Quaternion.identity);
        score = score + aScore;
    }
}
