using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletGenerator bulletGenerator;
    public bool enable;
    public BulletType type;
    public int element;
    public float speed;
    public float damage;
    public int[] bulletLevel;
    public bool isSplitPlus = false;

    [SerializeField] private GameObject[] sprites;

    private int pierceCount;
    private float time = 0;
    private GameObject[] enemies;
    private Vector3 targetPos;
    private bool isSplit = false;

    void Update()
    {
        if(enable)
        {
            SetUp();

            // 誘導
            if(type == BulletType.Homing)
            {
                if(targetPos != new Vector3(0, 50, 0) && time < 0.5f)
                {
                    Vector3 direction = targetPos - this.transform.position;
                    float deltaAngle = Mathf.DeltaAngle(this.transform.eulerAngles.z, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90);
                    this.transform.rotation *= Quaternion.Euler(0, 0, deltaAngle * Time.deltaTime * 10);
                }
            }

            if(type != BulletType.Split && type != BulletType.Chain && type != BulletType.Homing && !isSplit && bulletLevel[(int)BulletType.Split] > 0) StartCoroutine(Split());

            this.transform.position += this.transform.up * speed * Time.deltaTime;

            if(Mathf.Abs(this.transform.position.x + 2) > 4 || Mathf.Abs(this.transform.position.y) > 4.5f) DestroyBullet();
            time += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enable)
        {
            if(other.CompareTag("Enemy"))
            {
                var isDie = other.GetComponent<Enemy.EnemyBase>().HitPlayerBullet(damage);
                if(type != BulletType.Chain)
                {
                    int chainLevel = bulletLevel[(int)BulletType.Chain];
                    if((isDie && chainLevel > 0) || isSplitPlus) //for(int i = 0; i < 4; i++) bulletGenerator.ShotPlayerBullet(this.transform.position, 90 * i, 5, 1);
                    {
                        Research();
                        Vector3 direction = targetPos - (this.transform.position + this.transform.up);
                        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
                        switch(chainLevel)
                        {
                            case 1 :
                                for(int i = 0; i < 4; i++) bulletGenerator.ShotPlayerBullet(this.transform.position + this.transform.up * 0.75f, angle + 90 * i, 5, damage * 0.8f, BulletType.Chain);
                                break;
                            case 2 :
                                for(int i = 0; i < 6; i++) bulletGenerator.ShotPlayerBullet(this.transform.position + this.transform.up * 0.75f, angle + 60 * i, 5, damage * 0.9f, BulletType.Chain);
                                break;
                            case 3 :
                                for(int i = 0; i < 8; i++) bulletGenerator.ShotPlayerBullet(this.transform.position + this.transform.up * 0.75f, angle + 45 * i, 5, damage * 0.9f, BulletType.Chain);
                                break;
                        }
                        DestroyBullet();
                    }
                }
                if(pierceCount >= bulletLevel[(int)BulletType.Pierce] || type == BulletType.Homing) DestroyBullet();
                pierceCount++;
            }
        }
    }

    private void DestroyBullet()
    {
        enable = false;
        bulletGenerator.playerBulletEnable[element] = false;
        this.transform.position = new Vector3(0, 0, 50);
        pierceCount = 0;
        time = 0;
        isSplit = false;
    }

    public void Research()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float near = 50;
        Vector3 target = new Vector3(0, 50, 0);
        foreach(GameObject enemy in enemies)
        {
            if(enemy == this.gameObject) continue;
            float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
            if(near > distance && enemy.GetComponent<Enemy.EnemyBase>().Enable && Mathf.Abs(enemy.transform.localPosition.x) <= 4f && Mathf.Abs(enemy.transform.localPosition.y) <= 4.5f)
            {
                near = distance;
                target = enemy.transform.position;
            }
        }
        targetPos = target;
    }

    public void SetUp()
    {
        for(int i = 0; i < sprites.Length; i++) if(i != (int)BulletType.Pierce) sprites[i].SetActive(i == (int)type);
    }

    private IEnumerator Split()
    {
        isSplit = true;
        yield return new WaitForSeconds(0.1f);
        if(!enable) yield break;
        float angle = 0;
        switch(bulletLevel[(int)BulletType.Split])
        {
            case 1:
                bulletGenerator.ShotPlayerBullet(this.transform.position, angle + 5, speed, damage * 0.6f, BulletType.Split);
                bulletGenerator.ShotPlayerBullet(this.transform.position, angle - 5, speed, damage * 0.6f, BulletType.Split);
                break;
            case 2:
                bulletGenerator.ShotPlayerBullet(this.transform.position, angle + 15, speed, damage * 0.6f, BulletType.Split);
                bulletGenerator.ShotPlayerBullet(this.transform.position, angle + 0, speed, damage * 0.6f, BulletType.Split);
                bulletGenerator.ShotPlayerBullet(this.transform.position, angle - 15, speed, damage * 0.6f, BulletType.Split);
                break;
            case 3:
                for (int i = -2; i <= 2; i++) bulletGenerator.ShotPlayerBullet(this.transform.position, angle + 20 * i, speed, damage * 0.8f, BulletType.Split);
                break;
        }
        DestroyBullet();
    }
}
