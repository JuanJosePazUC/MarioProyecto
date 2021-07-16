using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private GameObject followPlayer;
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        followPlayer = GameObject.FindGameObjectWithTag("Player");
    }
}
