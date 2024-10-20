using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Bullet
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private GameObject sprite;
        private EnemyBulletBase _bulletBase;
        
        public bool Enable { get; private set; }
    
        public void Init(EnemyBulletBase bulletBase, Vector3 pos, float angle)
        {
            _bulletBase = bulletBase;
            transform.position = pos;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            sprite.SetActive(true);
            Enable = true;
        }

        private void Update()
        {
            if (!Enable) return;
            
            _bulletBase.Action(this.transform);

            if (Mathf.Abs(this.transform.position.x + 2) > 4 || Mathf.Abs(this.transform.position.y) > 4.5f) DestroyBullet();
        }

        private void DestroyBullet()
        {
            sprite.SetActive(false);
            Enable = false;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && other.TryGetComponent<Player>(out var player))
            {
                player.Hit(_bulletBase.GetDamage());
                DestroyBullet();
            }
            else if (other.CompareTag("Bomb"))
            {
                DestroyBullet();
            }
        }
    }
}

