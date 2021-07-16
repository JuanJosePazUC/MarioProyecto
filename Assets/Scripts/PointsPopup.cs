using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPopup : MonoBehaviour
{
    public string layername;
    public int SortingOrder;
    public float lifeTime = 0.7f;
    private void Start() {
        gameObject.GetComponentInChildren<MeshRenderer>().sortingLayerName = layername;
        gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = SortingOrder;
        Destroy(gameObject, lifeTime);
    }
}
