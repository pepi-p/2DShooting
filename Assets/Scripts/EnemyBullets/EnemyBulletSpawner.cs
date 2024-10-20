using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Bullet
{
    public class EnemyBulletSpawner : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private EnemyBullet _bulletPrefab;
        private List<EnemyBullet> _bullets = new List<EnemyBullet>();

        public void Spawn(EnemyBulletBase bulletBase, Vector3 pos, float angle)
        {
            foreach (var bullet in _bullets)
            {
                if (!bullet.Enable)
                {
                    bullet.Init(bulletBase, pos, angle);
                    return;
                }
            }

            var newBullet = Instantiate(_bulletPrefab);
            newBullet.Init(bulletBase, pos, angle);
            _bullets.Add(newBullet);
        }
    }
}