using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCollision : MonoBehaviour
{
    public GameObject mouseCollisionPrefab; // �浹 ������Ʈ ������ (������ �ݶ��̴�+������ٵ� ����)

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
            Debug.LogWarning("mouseCollisionPrefab�� �������� �ʾҽ��ϴ�!");
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
