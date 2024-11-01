using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EnemyBullet = global::Enemy.Bullet;

namespace LS.Enemy
{
    public class Diffusion : global::Enemy.EnemyAction
    {
        private Vector3 _startPos;
        private Vector3 _entryPos;
        private int _bulletAmount;
        private int _angle;

        public Diffusion(Player player, EnemyBullet.EnemyBulletSpawner bulletSpawner, Vector3 startPos, int bulletAmount = 6, int angle = 17) : base(player, bulletSpawner)
        {
            _startPos = EnemyCalc.ToWorldPos(startPos);
            _bulletAmount = bulletAmount;
            _angle = angle;
        }

        public override async void Action(Transform transform)
        {
            var token = cancelToken.Token;
            
            var initMove = InitMove(transform, token);
            
            try { await UniTask.WaitUntil(() => initMove.GetAwaiter().IsCompleted, cancellationToken: token); }
            catch (OperationCanceledException) { return; }
            
            var main = Main(transform, token);
            
            try { await UniTask.WaitUntil(() => main.GetAwaiter().IsCompleted, cancellationToken: token); }
            catch (OperationCanceledException) { return; }
            
            var end = EndMove(transform, token);
            
            try { await UniTask.WaitUntil(() => end.GetAwaiter().IsCompleted, cancellationToken: token); }
            catch (OperationCanceledException) { return; }
            
            EndEnemy();
        }

        private async UniTask Main(Transform transform, CancellationToken token)
        {
            var normal = new EnemyBullet.Normal(_player, 3, 1);
            var angle = 180;
            
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j <= _bulletAmount; j++)
                {
                    _bulletSpawner.Spawn(normal, transform.position, angle + 360 / _bulletAmount * j);
                }
                try { await UniTask.WaitForSeconds(0.25f, cancellationToken: token); }
                catch (OperationCanceledException) { return; }

                angle += _angle;
            }
        }

        private async UniTask InitMove(Transform transform, CancellationToken token)
        {
            _entryPos = EnemyCalc.ToWorldPos(Vector3.right * (Mathf.Sign(_startPos.x) * 5) + Vector3.up * _startPos.y);
            
            float time = 0f;
            while (time < 1)
            {
                try { await UniTask.WaitForSeconds(0.01f, cancellationToken: token); }
                catch (OperationCanceledException) {}
                time += 0.01f;
                transform.position = Vector2.Lerp(_entryPos, _startPos, time / 1f);
            }
        }
        
        private async UniTask EndMove(Transform transform, CancellationToken token)
        {
            float time = 0f;
            while (time < 1)
            {
                try { await UniTask.WaitForSeconds(0.01f, cancellationToken: token); }
                catch (OperationCanceledException) {}
                time += 0.01f;
                transform.position = Vector2.Lerp(_startPos, _entryPos, time / 1f);
            }
        }
    }
}