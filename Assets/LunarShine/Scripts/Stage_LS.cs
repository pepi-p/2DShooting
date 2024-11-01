using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Enemy;
using Enemy.Bullet;

public class Stage_LS : MonoBehaviour
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
        /*
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3( 0, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
        });
        
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
        });
        
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3( 3, -2.5f, 0)), 10);
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3(-3, -2.5f, 0)), 10);
        });
        */
/*
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3( 0, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
        });

        waves.Add(() =>
        {
            _enemySpawner.Spawn(new LS.Enemy.Diffusion(_player, _enemyBulletSpawner, new Vector3(0, 2.5f, 0), 10), 30);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new Constraint(_player, _enemyBulletSpawner, new Vector3(2, 2.5f, 0)), 10);
        });

        waves.Add(() => 
        {
            _enemySpawner.Spawn(new LS.Enemy.RandomEnemy(_player, _enemyBulletSpawner, new Vector3(0, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new LS.Enemy.RandomEnemy(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0)), 10);
            _enemySpawner.Spawn(new LS.Enemy.RandomEnemy(_player, _enemyBulletSpawner, new Vector3(2, 2.5f, 0)), 10);
        });

        waves.Add(() =>
        {
            _enemySpawner.Spawn(new LS.Enemy.ReflectionDiffusion(_player, _enemyBulletSpawner, new Vector3(0, 2.5f, 0), 5), 10);
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3(-2.5f, 3.5f, 0)), 7);
            _enemySpawner.Spawn(new Enemy.Normal(_player, _enemyBulletSpawner, new Vector3(2.5f, 3.5f, 0)), 7);
        });

        waves.Add(() =>
        {
            _enemySpawner.Spawn(new LS.Enemy.Diffusion(_player, _enemyBulletSpawner, new Vector3( 2, 2.5f, 0), 20), 20);
            _enemySpawner.Spawn(new LS.Enemy.Diffusion(_player, _enemyBulletSpawner, new Vector3(-2, 2.5f, 0), 20), 20);
            _enemySpawner.Spawn(new LS.Enemy.Diffusion(_player, _enemyBulletSpawner, new Vector3(0, 3.5f, 0), 30, 43), 20);
        });
*/
        waves.Add(() =>
        {
            _enemySpawner.Spawn(new LS.Enemy.BulletTester(_player, _enemyBulletSpawner, new Vector3(0, 3.5f, 0)), 20);
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