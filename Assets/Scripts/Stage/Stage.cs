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
    [SerializeField] private Tutorial _tutorial;
    private int currentWave;
    
    private delegate void Wave();
    private List<Wave> waves = new List<Wave>();

    private void Awake()
    {
        _tutorial.stageStart = () => Init();
    }

    public void Init()
    {
        waves.Add(() =>
        {
            // _enemySpawner.Spawn(new Circle(_player, _enemyBulletSpawner, new Vector3( 2, 2, 0), new AccelerationAngle(_player, 1, 1, 45), 30), 10f);
            _enemySpawner.Spawn(new Circle(_player, _enemyBulletSpawner, new Vector3(0, 2, 0), new Acceleration(_player, 1, 0, 1.5f), 30), 10f);
            // _enemySpawner.Spawn(new Accelerator(_player, _enemyBulletSpawner, new Vector3( 0, 2f, 0)), 10);
        });
        
        /*
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
        */

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
