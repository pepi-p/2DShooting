using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyCalc
{
    public static float GetPlayerAngle(Transform transform, Player player)
    {
        return 180 + Mathf.Atan2(player.transform.position.x - transform.position.x, transform.position.y - player.transform.position.y) * Mathf.Rad2Deg;
    }

    public static Vector3 ToLocalPos(Vector3 worldPos)
    {
        return worldPos + Vector3.right * 2;
    }

    public static Vector3 ToWorldPos(Vector3 localPos)
    {
        return localPos - Vector3.right * 2;
    }
}
