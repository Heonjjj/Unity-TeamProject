using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossAttackPattern
{
    float Cooldown { get; }
    float Damage { get; }
    void Activate();
}
