using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    Circle,
    Triangle,
    Square,
    Pentagon,
    Hexagon
};

public class Enemy : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] private Player player;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private EnemyBullet enemyBullet;

    [Space(5), Header("Prefab")]
    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private Item heart;
    [SerializeField] private Item pointItem;

    [Space(5), Header("Setting")]
    public float shotInterval;
    [SerializeField] private EnemyType type;
    [SerializeField] private bool turnPlayer;
    [SerializeField] private bool hitFlgReset;
    [SerializeField] private bool shotEnable;
    [SerializeField] private float speedMultiply = 1f;
    public float maxHP;

    [Space(5), Header("Attach")]
    [SerializeField] private GameObject[] sprites;

    private Collider2D col;
    private float shotCoolDown = 0;
    private float hp;
    public bool enable = true;
    private bool isScript = false; // スクリプトから生成されたかどうか
    
    void Start()
    {
        col = GetComponent<Collider2D>();
        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(i == (int)type);
        }
    }

    void Update()
    {
        if(enable)
        {
            if(turnPlayer) this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(player.transform.position.y - this.transform.position.y, player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg);
            if(shotCoolDown > 0) shotCoolDown -= Time.deltaTime;
            else if(shotEnable) Shot();
        }
        if(hitFlgReset) enable = true;
        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(i == (int)type);
        }
        sprites[(int)type].SetActive(enable);
        col.enabled = enable;
    }

    private void Shot()
    {
        shotCoolDown = shotInterval;
        switch(type)
        {
            case EnemyType.Circle:
                for(int i = 0; i < 3; i++)
                {
                    BulletShot(this.transform.position, -10 + 10 * i, 3 * speedMultiply);
                }
                break;
            case EnemyType.Triangle:
                for(int i = 0; i < 6; i++)
                {
                    BulletShot(this.transform.position, 0, 2 + i);
                }
                break;
            case EnemyType.Square:
                for(int i = 0; i < 4; i++)
                {
                    BulletShot(this.transform.position, 90 * i, 3 * speedMultiply);
                }
                break;
            case EnemyType.Pentagon:

                break;
            case EnemyType.Hexagon:
                for(int i = 0; i < 12; i++)
                {
                    BulletShot(this.transform.position, 30 * i, 3 * speedMultiply);
                }
                break;
            
        }
    }

    private void BulletShot(Vector3 position, float angle, float speed)
    {
        var pos = position;
        pos.z = 0;
        var bullet = Instantiate(enemyBullet, pos, Quaternion.Euler(0, 0, this.transform.eulerAngles.z - 90 + angle));
        bullet.speed = speed;
        bullet.type = (EnemyBullet.BulletType)type;
    }

    public bool HitPlayerBullet(float shotDamage)
    {
        hp += shotDamage;
        if(hp < maxHP) return false;
        // levelManager.GetExp(5);
        levelManager.GetPoint(100);
        enable = false;
        /*
        for(int i = 0; i < 4; i++)
        {
            BulletShot(this.transform.position, (360f / 4) * i, 5);
        }
        */
        int itemCount = 1;
        if(type == EnemyType.Hexagon) itemCount = Random.Range(4, 7);
        for(int i = 0; i < itemCount; i++)
        {
            if(Random.Range(0, 2) == 0) SpawnHeart(10);
            else SpawnPoint(500);
        }
        Instantiate(destroyParticle, this.transform.position, Quaternion.identity);
        if(isScript) Destroy(this.gameObject);
        hp = 0;
        return true;
    }

    private void SpawnHeart(float amount)
    {
        var hpItem = Instantiate(heart, this.transform.position + RandomVector3(0.5f), Quaternion.identity);
        hpItem.levelManager = levelManager;
        hpItem.player = player;
        hpItem.amount = amount;
    }

    private void SpawnPoint(float amount)
    {
        var ptItem = Instantiate(pointItem, this.transform.position + RandomVector3(0.5f), Quaternion.identity);
        ptItem.levelManager = levelManager;
        ptItem.player = player;
        ptItem.amount = amount;
    }

    public void SetUp(Player _player, LevelManager _levelManager, EnemyType _type, float _maxHP, float _shotInterval, float _speedMultiply, bool _turnPlayer, float _shotEnableDelay)
    {
        enable = true;
        isScript = true;
        player = _player;
        levelManager = _levelManager;
        type = _type;
        maxHP = _maxHP;
        hp = 0;
        shotInterval = _shotInterval;
        speedMultiply = _speedMultiply;
        turnPlayer = _turnPlayer;

        if(_shotEnableDelay < 0) shotEnable = false;
        else StartCoroutine(ShotEnableDelay(_shotEnableDelay));
    }

    private IEnumerator ShotEnableDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        shotCoolDown = 0;
        shotEnable = true;
        if(turnPlayer) this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(player.transform.position.y - this.transform.position.y, player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg);
        Shot();
    }

    private Vector3 RandomVector3(float radius)
    {
        var theta = Random.Range(-180f, 180f);
        return new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bomb"))
        {
            HitPlayerBullet(20);
        }
    }
}
