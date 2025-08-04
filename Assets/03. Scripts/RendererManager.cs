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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �����ϰ� ������ �߰�
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
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
        sortableObjects.Clear(); // ���� ��� ��� �ʱ�ȭ
        RegisterObjectsByTag("AutoSort"); // �� �� �±� ������Ʈ �ٽ� ���
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
        obj.tag = "AutoSort"; // �±װ� �ʿ��ϸ� ����
        RegisterObject(obj.transform);
    }
}
