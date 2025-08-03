using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererManager : MonoBehaviour
{
    public List<Transform> sortableObjects = new List<Transform>(); // Inspector에서 드래그

    void Start()
    {
        GameObject[] toSort = GameObject.FindGameObjectsWithTag("AutoSort");
        foreach (var obj in toSort)
        {
            sortableObjects.Add(obj.transform);
        }
    }

    void LateUpdate()
    {
        foreach (Transform obj in sortableObjects)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = Mathf.RoundToInt(-obj.position.y * 100);
            }
        }
    }
}
