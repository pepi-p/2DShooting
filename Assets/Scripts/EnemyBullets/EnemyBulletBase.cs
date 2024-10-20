using System.Collections;
using System.Collections.Generic;
using Enemy.Bullet;
using UnityEngine;

public abstract class EnemyBulletBase
{
    protected Player _player;
    protected float _speed;
    protected float _damage;

    public EnemyBulletBase(Player player, float speed, float damage)
    {
        _player = player;
        _speed = speed;
        _damage = damage;
    }
    
    public virtual void Action(Transform transform) {}

    public float GetDamage()
    {
        return _damage;
    }
}
