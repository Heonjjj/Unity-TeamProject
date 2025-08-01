using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCollisionFollower : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Hit {other.gameObject.name}");
        Destroy(other.gameObject);
    }
}
