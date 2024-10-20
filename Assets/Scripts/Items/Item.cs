using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Player _player;
    protected LevelManager _levelManager;
    private float _speed;
    private bool _isCollect;

    public void Init(Player player, LevelManager levelManager)
    {
        _player = player;
        _levelManager = levelManager;
    }
    
    private void Update()
    {
        if (_isCollect)
        {
            _speed = 3;
            this.transform.position += (_player.transform.position - this.transform.position).normalized * (_speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(this.transform.position, _player.transform.position) <= 1.0f) _isCollect = true;
            this.transform.position += Vector3.down * (_speed * Time.deltaTime);
            _speed = Mathf.Lerp(_speed, 3, Time.deltaTime);
        }

        if (this.transform.position.y < -5f) Destroy(this.gameObject);
    }

    protected virtual void ItemEffect() {}
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            _isCollect = true;
        }
        if (other.CompareTag("Player"))
        {
            ItemEffect();
            Destroy(this.gameObject);
        }
    }
}
