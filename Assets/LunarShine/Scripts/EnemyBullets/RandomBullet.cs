using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LS.Enemy.Bullet
{
    public class RandomBullet : EnemyBulletBase
    {
        private float _frame;
        private float _rand;
        public RandomBullet(Player player, float speed, float damage, float frame1, float frame2) : base(player, speed, damage)
        {
            _frame = Random.Range(frame1, frame2);
        }

        public override void Action(Transform transform)
        {
            if (_frame < 0) transform.position += transform.up * (_speed * Time.deltaTime);
            _frame--;
        }
    }
}