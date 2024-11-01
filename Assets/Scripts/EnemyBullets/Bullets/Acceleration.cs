using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Bullet
{
    public class Acceleration : EnemyBulletBase
    {
        private float _acceleration;
        private float _time;

        public Acceleration(Player player, float speed, float damage, float acceleration) : base(player, speed, damage)
        {
            _acceleration = acceleration;
            _time = 0;
        }
        
        public override void Init(Transform transform)
        {
            _time = 0;
            Debug.Log(transform.position);
        }
        
        public override void Action(Transform transform)
        {
            var speed = _speed + _acceleration * _time;
            transform.position += transform.up * (speed * Time.deltaTime);
            _time += Time.deltaTime;
        }
    }
}