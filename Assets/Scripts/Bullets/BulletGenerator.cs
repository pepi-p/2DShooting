using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] private Bullet playerBulletPrefab;

    private Bullet[] playerBullets = new Bullet[500];
    public bool[] playerBulletEnable = new bool[500];

    public int[] bulletLevel;
    public bool isSplitPlus;

    void Start()
    {
        for(int i = 0; i < 500; i++)
        {
            playerBullets[i] = Instantiate(playerBulletPrefab, new Vector3(0, 0, 50), Quaternion.identity, this.transform);
            playerBullets[i].bulletGenerator = this;
        }
    }

    public void ShotPlayerBullet(Vector3 position, float angle, float speed, float damage, BulletType type)
    {
        var pos = position;
        pos.z = 0;
        int element = 0;
        for(int i = 0; i < 500; i++)
        {
            if(!playerBulletEnable[i])
            {
                element = i;
                break;
            }
        }
        playerBulletEnable[element] = true;
        playerBullets[element].element = element;
        playerBullets[element].type = type;
        playerBullets[element].speed = speed;
        playerBullets[element].damage = damage;
        playerBullets[element].isSplitPlus = isSplitPlus;
        playerBullets[element].bulletLevel = bulletLevel;
        playerBullets[element].transform.rotation = Quaternion.Euler(0, 0, angle);
        playerBullets[element].transform.position = pos;
        playerBullets[element].enable = true;
        playerBullets[element].SetUp();
        if(type == BulletType.Homing) playerBullets[element].Research();
    }
}
