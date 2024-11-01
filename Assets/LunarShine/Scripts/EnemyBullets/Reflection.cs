using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS.Enemy.Bullet
{
    public class Reflection : EnemyBulletBase
    {
        private float _reflection;
        public Reflection(Player player, float speed, float damage, float reflection) : base(player, speed, damage)
        {
            _reflection = reflection;
        }

        public override void Action(Transform transform)
        {
            float z = transform.rotation.eulerAngles.z;

            if(Mathf.Abs(EnemyCalc.ToLocalPos(transform.position).x) >= 3.95f && _reflection > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -z);
                _reflection--;
            }

            transform.position += transform.up * (_speed * Time.deltaTime);
        }
    }
}