using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointItem : Item
{
    [SerializeField] private int pointAmount;
    
    protected override void ItemEffect()
    {
        base._levelManager.point += pointAmount;
    }
}
