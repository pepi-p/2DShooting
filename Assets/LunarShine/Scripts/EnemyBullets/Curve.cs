using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS.Enemy.Bullet
{
    public class Curve : EnemyBulletBase
    {
        private float _curveSpeed;
        public Curve(Player player, float speed, float damage, float curveSpeed) : base(player, speed, damage)
        {
            _curveSpeed = curveSpeed;
        }

        public override void Action(Transform transform)
        {
            transform.rotation *= Quaternion.Euler(0, 0, _curveSpeed * Time.deltaTime);
            transform.position += transform.up * (_speed * Time.deltaTime);
        }
    }
}