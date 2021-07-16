using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaDeath : MonoBehaviour
{
    public float lifeTime = 0.8f;

    private void Start() {
        Destroy(gameObject,lifeTime);
    }
}
