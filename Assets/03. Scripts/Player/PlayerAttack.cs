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

    public bool multiShot3 = false;                         //3���� ȭ��
    public bool multiShot2 = false;                         //��Ƽ��

    private int attackCount = 0;
    private int multiShotTriggerCount = 5;

    [SerializeField] private Transform weaponPivot;

    void Start()
    {
        character = GetComponent<Character>();
        move = GetComponent<PlayerMove>();

        rangeY = character.attackRange;
        rangeX = character.attackRange * rangeXMultiplier;
    }

    void Update()
    {
        GameObject target = FindEnemyInBoxRange();
        if (target != null)
        {
            AimWeaponAt(target.transform.position);

            if (Time.time - lastAttackTime >= character.attackSpeed)
            {
                attackCount++;

                if (multiShot3)
                {
                    if (multiShot2 && attackCount % multiShotTriggerCount == 0)
                    {
                        StartCoroutine(ShootMultiShot3DoubleWithDelay(target.transform));
                    }

                    else
                    {
                        ShootMultiShot3(target.transform);
                    }
                }

                else
                {
                    if (multiShot2 && attackCount % multiShotTriggerCount == 0)
                    {
                        StartCoroutine(ShootDoubleArrowWithDelay(target.transform));
                    }

                    else
                    {
                        ShootArrow(target.transform);
                    }
                }

                lastAttackTime = Time.time;
            }
        }
    }

    //���Ⱑ ���ݹ������� ���ϵ��� ��
    void AimWeaponAt(Vector3 targetPosition)
    {
        if (weaponPivot == null)
            return;

        Vector2 direction = (targetPosition - weaponPivot.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float weaponOffset = 0.1f;
        weaponPivot.localPosition = direction * weaponOffset;

        weaponPivot.rotation = Quaternion.Euler(0, 0, angle + 225f);
    }

    //��Ÿ� �� �� ã��
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

    //�⺻ ���� 
    void ShootArrow(Transform target)
    {
        Vector2 dir = (target.position - firePoint.position).normalized;
        ShootArrowInDirection(dir);
    }

    //��Ƽ�� ����
    IEnumerator ShootDoubleArrowWithDelay(Transform target)
    {
        Vector2 dir = (target.position - firePoint.position).normalized;
        ShootArrowInDirection(dir);
        yield return new WaitForSeconds(0.2f);
        ShootArrowInDirection(dir);
    }

    //����ȭ�� ���� 
    void ShootMultiShot3(Transform target)
    {
        Vector2 dir = (target.position - firePoint.position).normalized;
        float angle = 15f;
        ShootArrowInDirection(dir);
        ShootArrowInDirection(Quaternion.Euler(0, 0, angle) * dir);
        ShootArrowInDirection(Quaternion.Euler(0, 0, -angle) * dir);
    }

    //���� + ��Ƽ��, ��Ƽ���� ������
    IEnumerator ShootMultiShot3DoubleWithDelay(Transform target)
    {
        Vector2 dir = (target.position - firePoint.position).normalized;
        float angle = 15f;
        Vector2[] dirs = { dir, Quaternion.Euler(0, 0, angle) * dir, Quaternion.Euler(0, 0, -angle) * dir };

        foreach (var d in dirs)
            ShootArrowInDirection(d);

        yield return new WaitForSeconds(0.2f);

        foreach (var d in dirs)
            ShootArrowInDirection(d);
    }

    //�⺻ Ȱ ����
    void ShootArrowInDirection(Vector2 dir)
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        arrow.GetComponent<Rigidbody2D>().velocity = dir * 10f;

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetOwner(character);
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // �÷��̾� ������Ʈ ����, �߽ɿ��� ��Ÿ� �ڽ� �׸���
        Gizmos.color = Color.green;

        // character �� null ���� (�����Ϳ��� �ٷ� �׸� �� �ְ�)
        float showRangeY = 6f;          // ����Ʈ�� (Inspector���� ����)
        float showRangeX = 6f * 1.8f;   // ����Ʈ��

        if (character != null)
        {
            showRangeY = character.attackRange;
            showRangeX = character.attackRange * rangeXMultiplier;
        }

        Vector3 center = transform.position;
        Vector3 size = new Vector3(showRangeX, showRangeY, 0);

        Gizmos.DrawWireCube(center, size);
    }
#endif
}

