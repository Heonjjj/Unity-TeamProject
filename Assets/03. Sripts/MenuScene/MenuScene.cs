using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public List<GameObject> objectPrefabs;  // ������ 5�� �ֱ�
    public float spawnRangeX = 5f;
    public float spawnHeight = 6f; // ȭ�� ������ ����
    public int spawnCountPerPrefab = 5; // �����մ� ���� ����
    public float spawnInterval = 0.3f; // ������Ʈ �ϳ��� �������� ����

    void Start()
    {
        StartCoroutine(SpawnObjectsGradually());
    }

    IEnumerator SpawnObjectsGradually()
    {
        while (true)
        {
            // ������ ������ �ϳ� ����
            GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

            float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPos = new Vector3(spawnX, spawnHeight, 0);

            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            obj.AddComponent<FallingObjectCleaner>(); // ���⿡�� �߰�!

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Ŭ�� �� �� ��ȯ
        {
            //SceneLoader.LoadScene(Escene.NormalStage);
        }
    }
}
public class FallingObjectCleaner : MonoBehaviour
{
    public float destroyY = -6f;

    void Update()
    {
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
