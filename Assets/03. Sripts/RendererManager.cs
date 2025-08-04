using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class RendererManager : MonoBehaviour
{

    public static RendererManager Instance;
    private List<Transform> sortableObjects = new List<Transform>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지하고 싶으면 추가
        }
        else
        {
            Destroy(gameObject); // 중복 제거
        }
    }

    private void Start()
    {
        RendererManager.Instance?.RegisterObject(transform);
        RegisterObjectsByTag("AutoSort");
    }
    public void RegisterObject(Transform obj)
    {
        if (!sortableObjects.Contains(obj))
            sortableObjects.Add(obj);
    }

    void LateUpdate()
    {
        foreach (Transform obj in sortableObjects)
        {
            if (obj == null) continue;

            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr == null)
                sr = obj.GetComponentInChildren<SpriteRenderer>();

            if (sr == null)
            {
                continue;
            }

            int sortingOrder = Mathf.RoundToInt(-sr.transform.position.y * 100);
            sr.sortingOrder = sortingOrder;
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sortableObjects.Clear(); // 이전 등록 목록 초기화
        RegisterObjectsByTag("AutoSort"); // 씬 내 태그 오브젝트 다시 등록
    }
    public void RegisterObjectsByTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objs)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                RegisterObject(sr.transform);
                continue;
            }
            sr = obj.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
            {
                RegisterObject(sr.transform);
            }
        }
    }
    public void InstantiateAndRegister(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(prefab, position, rotation);
        obj.tag = "AutoSort"; // 태그가 필요하면 설정
        RegisterObject(obj.transform);
    }
}
