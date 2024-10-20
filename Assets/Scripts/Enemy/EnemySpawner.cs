using System.Collections;
using System.Collections.Generic;
using Enemy.Bullet;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private EnemyBulletSpawner _bulletSpawner;
        [SerializeField] private EnemyBase _enemyBasePrefab;

        private List<EnemyBase> _enemies = new List<EnemyBase>();

        public void Spawn(EnemyAction enemyAction, float maxHP)
        {
            foreach (var enemy in _enemies)
            {
                if (!enemy.Enable)
                {
                    enemy.Init(enemyAction, _player, _levelManager, maxHP);
                    return;
                }
            }

            var newEnemy = Instantiate(_enemyBasePrefab);
            newEnemy.Init(enemyAction, _player, _levelManager, maxHP);
            _enemies.Add(newEnemy);
        }

        public bool CheckAllEnemyEnable()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.Enable) return false;
            }

            return true;
        }
    }
}