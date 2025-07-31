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

    [SerializeField] private float rangeXMultiplier = 1.8f;  //16:9

    private float rangeX;
    private float rangeY;

    void Start()
    {
        character = GetComponent<Character>();
        move = GetComponent<PlayerMove>();

        rangeY = character.attackRange;
        rangeX = character.attackRange * rangeXMultiplier;
    }

    void Update()
    {
        if (Time.time - lastAttackTime >= character.attackSpeed)
        {
            GameObject target = FindEnemyInBoxRange();
            if (target != null)
            {
                ShootArrow(target.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    GameObject FindEnemyInBoxRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject near = null;
        float closestDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector2 diff = enemy.transform.position - transform.position;

            if (Mathf.Abs(diff.x) <= rangeX / 2f && Mathf.Abs(diff.y) <= rangeY / 2f)
            {
                float dist = diff.sqrMagnitude;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    near = enemy;
                }
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

