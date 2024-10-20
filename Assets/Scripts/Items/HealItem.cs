using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private float healAmount;
    
    protected override void ItemEffect()
    {
        base._player.Recovery(healAmount);
    }
}
