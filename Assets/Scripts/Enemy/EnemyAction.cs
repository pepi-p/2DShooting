using System.Collections;
using System.Collections.Generic;
using Enemy.Bullet;
using UnityEngine;
using System.Threading;

namespace Enemy
{
    public abstract class EnemyAction
    {
        protected Player _player;
        protected EnemyBulletSpawner _bulletSpawner;
        
        public delegate void End();
        public End endEnemy;
        public CancellationTokenSource cancelToken = new CancellationTokenSource();

        public EnemyAction(Player player, EnemyBulletSpawner bulletSpawner)
        {
            _player = player;
            _bulletSpawner = bulletSpawner;
        }
        
        public virtual void Action(Transform transform) {}

        protected void EndEnemy()
        {
            endEnemy.Invoke();
        }
    }
}