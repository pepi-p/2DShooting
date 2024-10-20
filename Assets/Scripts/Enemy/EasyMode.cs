using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Serialization;

public class EasyMode : MonoBehaviour
{
    /*
    [Header("Class")]
    [SerializeField] private Player player;
    [SerializeField] private LevelManager levelManager;

    [FormerlySerializedAs("enemy")]
    [Space(5), Header("Prefab")]
    [SerializeField] private Enemy enemyOld;

    [Space(5), Header("Path")]
    [SerializeField] private PathCreator[] path;

    private void Start()
    {
        StartCoroutine(SpawnTimeline());
    }

    private IEnumerator SpawnTimeline()
    {
        for(int i = 0; i < 5; i++)
        {
            Spawn(path[0], 4, EnemyType.Circle, 5, 1, 1, true, -1);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2);
        
        yield return new WaitForSeconds(0.2f);
        for(int i = 0; i < 5; i++)
        {
            if(i % 2 == 0)
            {
                Spawn(path[2], 4, EnemyType.Circle, 5, 5, 1, true, 0.5f);
                Spawn(path[3], 4, EnemyType.Circle, 5, 5, 1, true, 0.5f);
            }
            else
            {
                Spawn(path[2], 4, EnemyType.Circle, 5, 5, 1, false, -1);
                Spawn(path[3], 4, EnemyType.Circle, 5, 5, 1, false, -1);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Spawn(PathCreator _path, float _speed, EnemyType _type, float _maxHP, float _shotInterval, float _speedMultiply, bool _turnPlayer, float _shotEnableDelay)
    {
        var _enemy = Instantiate(enemyOld, new Vector3(0, 5, 0), Quaternion.identity);
        var _enemyPath = _enemy.GetComponent<EnemyPathMove>();
        _enemy.SetUp(player, levelManager, _type, _maxHP, _shotInterval, _speedMultiply, _turnPlayer, _shotEnableDelay);
        _enemyPath.pathCreator = _path;
        _enemyPath.speed = _speed;
    }
    */
}
