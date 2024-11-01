using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Bullet
{
    public class AccelerationAngle : EnemyBulletBase
    {
        private float _acceleration;
        private float _angle = 0;

        public AccelerationAngle(Player player, float speed, float damage, float acceleration) : base(player, speed, damage)
        {
            _acceleration = acceleration;
            _angle = 0;
        }

        public override void Init(Transform transform)
        {
            _angle = 0;
        }
        
        public override void Action(Transform transform)
        {
            if (_angle < 360f)
            {
                transform.rotation *= Quaternion.Euler(0, 0, _acceleration * Time.deltaTime);
                _angle += _acceleration * Time.deltaTime;
            }

            transform.position += transform.up * (_speed * Time.deltaTime);
        }
    }
}