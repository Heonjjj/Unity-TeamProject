using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Character owner;

    public void SetOwner(Character attacker)
    {
        owner = attacker;
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Character target = other.GetComponent<Character>();

        if (target != null && target != owner)
        {
            target.TakeDamage(owner.attackPower);
            Destroy(gameObject);
        }
    }

}
