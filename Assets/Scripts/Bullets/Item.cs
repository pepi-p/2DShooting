using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [System.NonSerialized] public Player player;
    [System.NonSerialized] public LevelManager levelManager;
    [System.NonSerialized] public float amount;
    [SerializeField] private ItemType type;
    private float speed = -3;
    private bool isCollect;

    private enum ItemType
    {
        Recovery,
        Point
    }

    void Update()
    {
        if(isCollect)
        {
            speed = 3;
            this.transform.position += (player.transform.position - this.transform.position).normalized * speed * Time.deltaTime;
        }
        else
        {
            if(Vector2.Distance(this.transform.position, player.transform.position) <= 1.0f) isCollect = true;
            this.transform.position += Vector3.down * speed * Time.deltaTime;
            speed = Mathf.Lerp(speed, 3, Time.deltaTime);
        }

        if(this.transform.position.y < -5f) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bomb"))
        {
            isCollect = true;
        }
        if(other.CompareTag("Player"))
        {
            switch(type)
            {
                case ItemType.Recovery :
                    player.Recovery(amount);
                    break;
                case ItemType.Point :
                    levelManager.point += (int)amount;
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
