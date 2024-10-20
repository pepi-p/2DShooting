using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Bullet;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Enemy
{
    public class Constraint : EnemyAction
    {
        private Vector3 _startPos;
        private Vector3 _entryPos;
        
        public Constraint(Player player, EnemyBulletSpawner bulletSpawner, Vector3 startPos) : base(player, bulletSpawner)
        {
            _startPos = EnemyCalc.ToWorldPos(startPos);
        }

        public override async void Action(Transform transform)
        {
            var token = cancelToken.Token;
            
            var initMove = InitMove(transform, token);
            
            try { await UniTask.WaitUntil(() => initMove.GetAwaiter().IsCompleted, cancellationToken: token); }
            catch (OperationCanceledException) {}
            
            var main = Main(transform, token);
            
            try { await UniTask.WaitUntil(() => main.GetAwaiter().IsCompleted, cancellationToken: token); }
            catch (OperationCanceledException) {}
            
            var end = EndMove(transform, token);
            
            try { await UniTask.WaitUntil(() => end.GetAwaiter().IsCompleted, cancellationToken: token); }
            catch (OperationCanceledException) {}
            
            EndEnemy();
        }

        private async UniTask Main(Transform transform, CancellationToken token)
        {
            var normalBullet = new Bullet.Normal(_player, 3, 1);
            
            for (int i = 0; i < 20; i++)
            {
                var angle = EnemyCalc.GetPlayerAngle(transform, _player);
                _bulletSpawner.Spawn(normalBullet, transform.position, angle + 10);
                _bulletSpawner.Spawn(normalBullet, transform.position, angle - 10);
                try { await UniTask.WaitForSeconds(0.25f, cancellationToken: token); }
                catch (OperationCanceledException) {}
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