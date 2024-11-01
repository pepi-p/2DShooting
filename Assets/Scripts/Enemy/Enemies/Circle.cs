using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Bullet;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Enemy
{
    public class Circle : EnemyAction
    {
        private Vector3 _startPos;
        private Vector3 _entryPos;
        private EnemyBulletBase _enemyBulletBase;
        private float _deltaAngle;
        
        public Circle(Player player, EnemyBulletSpawner bulletSpawner, Vector3 startPos, EnemyBulletBase bullet, float deltaAngle) : base(player, bulletSpawner)
        {
            _startPos = EnemyCalc.ToWorldPos(startPos);
            _enemyBulletBase = bullet;
            _deltaAngle = deltaAngle;
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
            for (int i = 0; i < 20; i++)
            {
                var angle = EnemyCalc.GetPlayerAngle(transform, _player);
                for (float j = 0; j < 360; j += _deltaAngle) _bulletSpawner.Spawn(_enemyBulletBase, transform.position, j);
                try { await UniTask.WaitForSeconds(0.2f, cancellationToken: token); }
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