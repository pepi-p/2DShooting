using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusUp : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] private HPManager hpManager;
    [SerializeField] private LevelManager levelManager;

    [Space(5), Header("Setting")]
    [SerializeField] private int hpRecoverPrice;
    [SerializeField] private int maxHPUpPrice;
    [SerializeField] private int addBombPrice;

    [SerializeField] private TextMeshProUGUI pricetxt;

    public void HPRecover(int amount)
    {
        var point = levelManager.point;
        if(point < hpRecoverPrice) return;
        levelManager.point -= hpRecoverPrice;
        hpManager.HP += amount;
    }

    public void MaxHPUP(int amount)
    {
        var point = levelManager.point;
        if(point < maxHPUpPrice) return;
        levelManager.point -= maxHPUpPrice;
        hpManager.MaxHPUP(amount);
        maxHPUpPrice += 500;
        pricetxt.text = "HP上限 +10\nPT : " + maxHPUpPrice;
    }

    public void AddBomb()
    {
        var point = levelManager.point;
        if(point < addBombPrice) return;
        levelManager.point -= addBombPrice;
        levelManager.AddBomb();
    }
}
