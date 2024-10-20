using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Bullet
{
    public class Normal : EnemyBulletBase
    {
        public Normal(Player player, float speed, float damage) : base(player, speed, damage) {}
        
        public override void Action(Transform transform)
        {
            transform.position += transform.up * (_speed * Time.deltaTime);
        }
    }
}