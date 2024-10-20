using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using Enemy;
using Enemy.Bullet;
using Normal = Enemy.Normal;

public class Stage : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private EnemyBulletSpawner _enemyBulletSpawner;
    private int currentWave;
    
    private delegate void Wave();
    private List<Wave> waves = new List<Wave>();

    public void Init()
    {
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new Normal(_player, _enemyBulletSpawner, new Vector3( 0, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
        });
        
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new Normal(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Normal(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
        });
        
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new Normal(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Normal(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Normal(_player, _enemyBulletSpawner, new Vector3( 3, -2.5f, 0)), 10);
            _enemySpawner.Spawn(new Normal(_player, _enemyBulletSpawner, new Vector3(-3, -2.5f, 0)), 10);
        });

        WaveStart();
    }

    private async void WaveStart()
    {
        while (true)
        {
            foreach (var wave in waves)
            {
                wave.Invoke();
                await UniTask.WaitUntil(() => _enemySpawner.CheckAllEnemyEnable());
            }
        }
    }
}
