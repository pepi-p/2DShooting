using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public BulletType type;
    public float speed;

    [SerializeField] private SpriteRenderer[] sprite;
    [SerializeField] private GameObject[] bulletImg;

    public enum BulletType {
        Circle,
        Triangle,
        Square,
        Pentagon,
        Hexagon
    };

    void Start()
    {
        Color spriteColor = new Color(0, 0, 0, 0);
        for(int i = 0; i < bulletImg.Length; i++) bulletImg[i].SetActive(false);
        switch(type)
        {
            case BulletType.Circle:
                spriteColor = new Color(1, 0.3764706f, 0.3764706f);
                bulletImg[0].SetActive(true);
                break;
            case BulletType.Triangle:
                spriteColor = new Color(0.3764706f, 1, 0.7529412f);
                bulletImg[0].SetActive(true);
                break;
            case BulletType.Square:
                spriteColor = new Color(0.3764706f, 0.7529412f, 1);
                bulletImg[0].SetActive(true);
                break;
            case BulletType.Pentagon:
                spriteColor = new Color(1, 1, 0.3764706f);
                bulletImg[0].SetActive(true);
                break;
            case BulletType.Hexagon:
                spriteColor = new Color(0.7529412f, 0.3764706f, 1);
                bulletImg[0].SetActive(true);
                break;
        }
        for(int i = 0; i < sprite.Length; i++) sprite[i].color = spriteColor;
    }

    void Update()
    {
        this.transform.position += this.transform.up * speed * Time.deltaTime;

        if(Mathf.Abs(this.transform.position.x + 2) > 4 || Mathf.Abs(this.transform.position.y) > 4.5f) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Hit(5f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Bomb"))
        {
            Destroy(this.gameObject);
        }
    }
}
