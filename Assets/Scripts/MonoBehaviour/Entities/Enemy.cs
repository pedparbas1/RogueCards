using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : TargetEntity
{
    public int customInt = 2658111;
    public int HP { get; private set; } = 100;

    public override void AddBuff(DamageType resist)
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(int dmg, DamageType type)
    {
        HP -= dmg;
        Debug.Log(HP);
    }

    public void Take()
    {
        TakeDamage(10,null);
    }
}
