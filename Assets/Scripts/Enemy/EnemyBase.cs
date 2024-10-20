using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private GameObject _sprite;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private GameObject destroyParticle;
        [SerializeField] private Item heartItem;
        [SerializeField] private Item pointItem;
        private Player _player;
        private LevelManager _levelManager;
        private EnemyAction _enemyAction;

        public HitPoint HP { get; private set; }
        public bool Enable { get; private set; }

        public void Init(EnemyAction enemyAction, Player player, LevelManager levelManager, float maxHP)
        {
            _enemyAction = enemyAction;
            _player = player;
            _levelManager = levelManager;
            HP = new HitPoint(maxHP);

            _sprite.SetActive(true);
            _collider.enabled = true;
            Enable = true;

            _enemyAction.endEnemy = () => HideEnemy();
            _enemyAction.Action(this.transform);
        }
        
        public bool HitPlayerBullet(float damage)
        {
            HP.Damage(damage);
            var isDie = HP.IsDie();
            if (isDie) DestroyEnemy();
            return isDie;
        }

        private void DestroyEnemy()
        {
            _sprite.SetActive(false);
            Instantiate(destroyParticle, this.transform.position, Quaternion.identity);

            int itemCount = Random.Range(1, 4);
            for (int i = 0; i < itemCount; i++)
            {
                SpawnItem(RandomBool() ? heartItem : pointItem);
            }

            _levelManager.GetPoint(100);

            HideEnemy();

            _enemyAction.cancelToken.Cancel();
        }

        private void HideEnemy()
        {
            _sprite.SetActive(false);
            _collider.enabled = false;
            Enable = false;
        }

        private void SpawnItem(Item item)
        {
            var _item = Instantiate(item, this.transform.position + RandomVector3(0.5f), Quaternion.identity);
            _item.Init(_player, _levelManager);
        }

        private Vector3 RandomVector3(float radius)
        {
            var theta = Random.Range(-180f, 180f);
            return new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0);
        }

        private bool RandomBool()
        {
            return Random.Range(0, 2) == 0;
        }
    }
}
