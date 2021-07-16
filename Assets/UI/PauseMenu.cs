using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    
    /*
    private void Start() {
        pauseMenuUI = GetComponent<GameObject>();
    }
    */
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            FindObjectOfType<AudioManager>().Play("Pause");
            if(gameIsPaused){
                FindObjectOfType<AudioManager>().Pause("MainTheme");
                Resume();
            }else{
                FindObjectOfType<AudioManager>().Pause("MainTheme");
                Pause();
            }
        }
    }

    void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
