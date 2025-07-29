using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Character character;
    private PlayerMove move;

    private float lastAttackTime = 0f;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemyLayer;

    void Awake()
    {
        character = GetComponent<Character>();
        move = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (Time.time - lastAttackTime >= character.attackSpeed)
        {
            GameObject target = FindNearEnemy();
            if (target != null)
            {
                ShootArrow(target.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    GameObject FindNearEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = Mathf.Infinity;
        GameObject near = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                near = enemy;
            }
        }

        return near;
    }

    void ShootArrow(Transform target)
    {
        Vector2 dir = (target.position - firePoint.position).normalized;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = dir * 10f;

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetOwner(character);
        }
    }
}
