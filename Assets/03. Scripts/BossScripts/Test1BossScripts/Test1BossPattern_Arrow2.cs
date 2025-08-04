using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1BossPattern_Arrow2 : BossProjectilePatternBase, IBossAttackPattern
{
    public float Damage => 1f;
    public float Cooldown => 5f;

    public void Activate()
    {
        ShootProjectiles(5, 8f, 90f, true, Damage);
    }
}
