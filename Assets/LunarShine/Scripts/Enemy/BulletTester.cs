using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EnemyBullet = global::Enemy.Bullet;

namespace LS.Enemy
{
    public class BulletTester : global::Enemy.EnemyAction
    {
        private Vector3 _startPos;
        private Vector3 _entryPos;

        public BulletTester(Player player, EnemyBullet.EnemyBulletSpawner bulletSpawner, Vector3 startPos) : base(player, bulletSpawner)
        {
            _startPos = EnemyCalc.ToWorldPos(startPos);
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
            var bullet = new LS.Enemy.Bullet.Curve(_player, 10, 1, 82);
            var angle = 120;
            
            for (int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    _bulletSpawner.Spawn(bullet, transform.position, angle + j * 60);
                }
                try { await UniTask.WaitForSeconds(0.1f, cancellationToken: token); }
                catch (OperationCanceledException) { return; }

                angle -= 17;
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