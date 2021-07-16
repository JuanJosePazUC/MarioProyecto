using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCoin : MonoBehaviour
{
    public float lifeTime = 1f;
    private void Start() {
        FindObjectOfType<AudioManager>().Play("Coin");
        Destroy(gameObject,lifeTime);
    }
}
