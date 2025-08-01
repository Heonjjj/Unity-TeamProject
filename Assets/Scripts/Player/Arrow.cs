using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    private Character owner;

    void Awake()
    {
        Debug.Log("[Arrow] Awake: " + gameObject.GetInstanceID());
    }
    void Start()
    {
        Debug.Log("[Arrow] Start: " + gameObject.GetInstanceID());
    }

    public void SetOwner(Character attacker)
    {
        owner = attacker;
        Debug.Log("[Arrow] SetOwner called for " + gameObject.GetInstanceID());
        Destroy(gameObject, 0.8f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[Arrow] Trigger: {other.gameObject.name}, parent: {other.transform.parent?.name}");

        Character target = other.GetComponent<Character>();
        Debug.Log($"[Arrow] BossCharacter found: {target}");

        if (target != null && target != owner)
        {
            Debug.Log("[Arrow] Hit Boss and will destroy Arrow");
            target.TakeDamage(owner.attackPower);
            Debug.Log("[Arrow] Destroy Called!!");
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            Debug.Log("[Arrow] Hit Wall/Obstacle and will destroy Arrow");
            Debug.Log("[Arrow] Destroy Called!!");
            Destroy(gameObject);
            return;
        }

        if (other.GetComponent<TilemapCollider2D>() != null)
        {
            Debug.Log("[Arrow] Hit TilemapCollider2D and will destroy Arrow");
            Debug.Log("[Arrow] Destroy Called!!");
            Destroy(gameObject);
            return;
        }
    }
    void OnDestroy()
    {
        Debug.Log("[Arrow] OnDestroy called for " + gameObject.GetInstanceID());
    }
}
