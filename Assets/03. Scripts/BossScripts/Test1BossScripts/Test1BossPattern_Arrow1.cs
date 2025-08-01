using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1BossPattern_Arrow1 : BossProjectilePatternBase, IBossAttackPattern
{
    public float Damage => 1f;
    public float Cooldown => 0f;

    public void Activate()
    {
        ShootProjectiles(1, 10f, 0f, false, Damage);
    }
}
