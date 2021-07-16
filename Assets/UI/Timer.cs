using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float TIMER = 400;
    public bool flag = true;
    private void Start() {
        timerText = GetComponent<Text>();
    }

    private void Update() {
        if(TIMER > 0){          
            TIMER -= Time.deltaTime;
        }

        timerText.text = TIMER.ToString("0");

        if(TIMER <= 0){
            FindObjectOfType<CombateMario>().life = 0;
            FindObjectOfType<AudioManager>().Pause("MainTheme");
            FindObjectOfType<AudioManager>().Pause("HurryMainTheme");
        }

        if(TIMER <= 100 && flag){
            FindObjectOfType<AudioManager>().Pause("MainTheme");
            FindObjectOfType<AudioManager>().Play("HurryMainTheme");
            flag = false;
        }

    }
}
