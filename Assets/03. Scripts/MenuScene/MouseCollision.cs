using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCollision : MonoBehaviour
{
    public GameObject mouseCollisionPrefab; // 충돌 오브젝트 프리팹 (투명한 콜라이더+리지드바디 포함)

    private GameObject spawnedMouseCollision;

    void Start()
    {
        SpawnMouseCollisionObject();
    }

    void SpawnMouseCollisionObject()
    {
        if (mouseCollisionPrefab != null)
        {
            spawnedMouseCollision = Instantiate(mouseCollisionPrefab);
        }
        else
        {
            Debug.LogWarning("mouseCollisionPrefab이 설정되지 않았습니다!");
        }
    }

    void OnDestroy()
    {
        if (spawnedMouseCollision != null)
        {
            Destroy(spawnedMouseCollision);
        }
    }
}
