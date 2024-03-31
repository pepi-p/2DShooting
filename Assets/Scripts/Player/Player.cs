using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] BulletGenerator bulletGenerator;
    [SerializeField] HPManager hpManager;
    [SerializeField] LevelManager levelManager;

    [Space(5), Header("Prefab")]
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private GameObject bombParticle;

    [Space(5), Header("Setting")]
    [SerializeField] private float speed;
    [SerializeField] private float shotInterval;
    public float damageMultiply = 1f;

    [Space(5), Header("Attach")]
    [SerializeField] private GameObject hantei;

    private float bullet_cooldown;

    public int[] bulletLevel;

    void Awake()
    {
        int length = System.Enum.GetValues(typeof(BulletType)).Length;
        bulletLevel = new int[length];
    }

    void Start()
    {
        bulletGenerator.bulletLevel = bulletLevel;
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        Vector3 playerPos = this.transform.position;
        playerPos.z = 0;

        var _speed = Input.GetKey(KeyCode.LeftShift) ? speed / 2f : speed;

        this.transform.position += new Vector3(horizontal, vertical).normalized * _speed * Time.deltaTime;
        this.transform.localPosition = new Vector3(Mathf.Clamp(this.transform.localPosition.x, -4, 4), Mathf.Clamp(this.transform.localPosition.y, -4.5f, 4.5f), this.transform.localPosition.z);

        hantei.SetActive(Input.GetKey(KeyCode.LeftShift));

        if(Input.GetKey(KeyCode.Z) && bullet_cooldown <= 0) Shot(playerPos);
        if(Input.GetKeyDown(KeyCode.X)) ShotBomb();

        if(bullet_cooldown > 0) bullet_cooldown -= Time.deltaTime;
    }

    private void Shot(Vector3 pos)
    {
        bullet_cooldown = shotInterval;

        // 貫通
        switch(bulletLevel[(int)BulletType.Pierce])
        {
            case 1 :
                break;
            case 2 :
                break;
            case 3 :
                break;
        }

        // 爆発
        switch(bulletLevel[(int)BulletType.Explosion])
        {
            case 1 :
                break;
            case 2 :
                break;
            case 3 :
                break;
        }

        // 拡散
        switch(bulletLevel[(int)BulletType.Spread])
        {
            case 0 :
                bulletGenerator.ShotPlayerBullet(pos, 0, 10, 1 * damageMultiply, BulletType.Normal);
                break;
            case 1 :
                for(int i = -1; i <= 1; i++) bulletGenerator.ShotPlayerBullet(pos, i * 10, 10, 1.0f * damageMultiply, BulletType.Spread);
                break;
            case 2 :
                for(int i = 0; i < 4; i++) bulletGenerator.ShotPlayerBullet(pos, i * 5 - 7.5f, 10, 1.0f * damageMultiply, BulletType.Spread);
                break;
            case 3 :
                for(int i = -2; i <= 2; i++) bulletGenerator.ShotPlayerBullet(pos, i * 5, 10, 1.2f * damageMultiply, BulletType.Spread);
                break;
        }

        // 分裂
        switch(bulletLevel[(int)BulletType.Split])
        {
            case 1 :
                break;
            case 2 :
                break;
            case 3 :
                break;
        }

        // 誘導
        switch(bulletLevel[(int)BulletType.Homing])
        {
            case 1 :
                bulletGenerator.ShotPlayerBullet(pos, 0, 10, 0.25f * damageMultiply, BulletType.Homing);
                break;
            case 2 :
                bulletGenerator.ShotPlayerBullet(pos + new Vector3(0.2f, -0.1f, 0), -60, 10, 0.25f * damageMultiply, BulletType.Homing);
                bulletGenerator.ShotPlayerBullet(pos + new Vector3(-0.2f, -0.1f, 0), 60, 10, 0.25f * damageMultiply, BulletType.Homing);
                break;
            case 3 :
                bulletGenerator.ShotPlayerBullet(pos, 0, 10, 0.5f * damageMultiply, BulletType.Homing);
                bulletGenerator.ShotPlayerBullet(pos + new Vector3(0.2f, -0.1f, 0), -60, 10, 0.5f * damageMultiply, BulletType.Homing);
                bulletGenerator.ShotPlayerBullet(pos + new Vector3(-0.2f, -0.1f, 0), 60, 10, 0.5f * damageMultiply, BulletType.Homing);
                break;
        }

        // 連鎖
        switch(bulletLevel[(int)BulletType.Chain])
        {
            case 1 :
                break;
            case 2 :
                break;
            case 3 :
                break;
        }
    }

    public void Hit(float damage)
    {
        hpManager.HP -= damage;
    }

    public void Recovery(float recoveryValue)
    {
        hpManager.HP += recoveryValue;
    }

    public void SetBulletLevel(BulletType type, int lv)
    {
        bulletLevel[(int)type] = lv;
        bulletGenerator.bulletLevel = bulletLevel;
    }

    private void ShotBomb()
    {
        if(levelManager.UseBomb()) Instantiate(bombParticle, this.transform.position, Quaternion.identity);
    }
}
