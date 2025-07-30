using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/BossStats", fileName = "NewBossStats")]
public class BossStats : ScriptableObject
{
    public string BossName;
    public float MaxHP = 100f;
    public float MoveSpeed = 2f;
}
