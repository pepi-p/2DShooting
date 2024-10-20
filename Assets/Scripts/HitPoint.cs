using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint
{
    public float CurrentHP { get; private set; }
    public float MaxHP { get; private set; }

    public HitPoint(float maxHP)
    {
        this.CurrentHP = maxHP;
        this.MaxHP = maxHP;
    }

    public float GetHPRate()
    {
        return CurrentHP / MaxHP;
    }

    public void Damage(float damage)
    {
        CurrentHP -= damage;
        Normalize();
    }
    
    public void Recovery(float amount)
    {
        CurrentHP += amount;
        Normalize();
    }

    private void Normalize()
    {
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);
    }

    public bool IsDie()
    {
        return CurrentHP <= 0;
    }
}
