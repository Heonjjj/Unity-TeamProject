using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset = new Vector3(0, 0, -10f);

    void Start()
    {
        TryAssingTarget();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            TryAssingTarget();
            if (target == null) return;

        }

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    void TryAssingTarget()
    {
        Player player = Player.Instance;
        if (player != null)
            target = player.transform;
    }
}
