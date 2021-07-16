using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldID : MonoBehaviour
{
    public Text worldID;
    private Scene scene;

    private void Start() {
        worldID = GetComponent<Text>();
        scene = SceneManager.GetActiveScene();
    }

    private void Update() {
        worldID.text = scene.name;
    }
}
