using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public List<GameObject> objectPrefabs;  // 프리팹 5개 넣기
    public float spawnRangeX = 5f;
    public float spawnHeight = 6f; // 화면 위에서 생성
    public int spawnCountPerPrefab = 5; // 프리팹당 생성 개수
    public float spawnInterval = 0.3f; // 오브젝트 하나씩 떨어지는 간격

    void Start()
    {
        StartCoroutine(SpawnObjectsGradually());
    }

    IEnumerator SpawnObjectsGradually()
    {
        while (true)
        {
            // 랜덤한 프리팹 하나 선택
            GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

            float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPos = new Vector3(spawnX, spawnHeight, 0);

            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            obj.AddComponent<FallingObjectCleaner>(); // 여기에서 추가!

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 클릭 시 씬 전환
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
