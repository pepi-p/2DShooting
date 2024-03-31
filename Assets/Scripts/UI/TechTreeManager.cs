using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeManager : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] private Player player;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Pose pose;
    [SerializeField] private BulletGenerator bulletGenerator;

    [Space(5), Header("Node")]
    [SerializeField] private TechTreeNode[] pierce;
    [SerializeField] private TechTreeNode[] explosion;
    [SerializeField] private TechTreeNode[] spread;
    [SerializeField] private TechTreeNode[] split;
    [SerializeField] private TechTreeNode[] homing;
    [SerializeField] private TechTreeNode[] chain;
    [SerializeField] private TechTreeNode splitPlus;

    [Space(5), Header("Edge")]
    [SerializeField] private Image[] branchEdgeExplosion;
    [SerializeField] private Image[] branchEdgeChain;
    [SerializeField] private Image[] branchEdgeHoming;
    [SerializeField] private Image[] branchEdgeSplit;
    [SerializeField] private Image[] branchEdgeSplitPuls;

    [Space(5), Header("Color")]
    public Color enableTextColor;
    public Color disableTextColor;
    public Color unlockedColor;
    public Color unlockAbleColor;
    public Color unlockedEdgeColor;
    public Color enableEdgeColor;
    public Color disableEdgeColor;

    private TechTreeNode[,] nodes = new TechTreeNode[7, 3];

    private bool splitPulsFlg1;
    private bool splitPulsFlg2;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            nodes[(int)BulletType.Pierce, i] = pierce[i];
            nodes[(int)BulletType.Explosion, i] = explosion[i];
            nodes[(int)BulletType.Spread, i] = spread[i];
            nodes[(int)BulletType.Split, i] = split[i];
            nodes[(int)BulletType.Homing, i] = homing[i];
            nodes[(int)BulletType.Chain, i] = chain[i];
        }
    }

    public void PushNode(string bulletType)
    {
        int price = 0;
        var split = bulletType.Split(' ');
        if(split[0] == "SplitPuls")
        {
            price = int.Parse(split[2]);
            var point = levelManager.point;
            if(point < price) return;
            else levelManager.point -= price;
            pose.UpdatePointDisplay();
            levelManager.UsePoint();
            bulletGenerator.isSplitPlus = true;
            splitPlus.UnlockNode();
            return;
        }
        var type = (BulletType)System.Enum.Parse(typeof(BulletType), split[0], true);
        var level = int.Parse(split[1]) - 1;
        if((level + 1) == player.bulletLevel[(int)type]) return;
        if(split.Length >= 3)
        {
            price = int.Parse(split[2]);
            var point = levelManager.point;
            if(point < price) return;
            else levelManager.point -= price;
            pose.UpdatePointDisplay();
            levelManager.UsePoint();
        }

        player.SetBulletLevel(type, level + 1);
        nodes[(int)type, level].UnlockNode();
        if(level < 2) nodes[(int)type, level + 1].EnableNode();
        else switch(type)
        {
            case BulletType.Pierce :
                nodes[(int)BulletType.Explosion, 0].EnableNode();
                nodes[(int)BulletType.Chain, 0].EnableNode();
                for(int i = 0; i < branchEdgeExplosion.Length; i++)
                {
                    branchEdgeExplosion[i].color = enableEdgeColor;
                    branchEdgeChain[i].color = enableEdgeColor;
                }
                break;
            case BulletType.Spread :
                nodes[(int)BulletType.Homing, 0].EnableNode();
                nodes[(int)BulletType.Split, 0].EnableNode();
                for(int i = 0; i < branchEdgeHoming.Length; i++)
                {
                    branchEdgeHoming[i].color = enableEdgeColor;
                    branchEdgeSplit[i].color = enableEdgeColor;
                }
                break;
            case BulletType.Chain :
                splitPulsFlg1 = true;
                break;
            case BulletType.Split :
                splitPulsFlg2 = true;
                break;
        }
        if(level > 0) nodes[(int)type, level - 1].UnlockEdge();
        else switch(type)
        {
            case BulletType.Explosion :
                for(int i = 0; i < branchEdgeExplosion.Length; i++) branchEdgeExplosion[i].color = unlockedEdgeColor;
                break;
            case BulletType.Chain :
                for(int i = 0; i < branchEdgeChain.Length; i++) branchEdgeChain[i].color = unlockedEdgeColor;
                break;
            case BulletType.Homing :
                for(int i = 0; i < branchEdgeHoming.Length; i++) branchEdgeHoming[i].color = unlockedEdgeColor;
                break;
            case BulletType.Split :
                for(int i = 0; i < branchEdgeSplit.Length; i++) branchEdgeSplit[i].color = unlockedEdgeColor;
                break;
        }
        if(splitPulsFlg1 && splitPulsFlg2)
        {
            for(int i = 0; i < branchEdgeSplitPuls.Length; i++) branchEdgeSplitPuls[i].color = unlockedEdgeColor;
            splitPlus.EnableNode();
        }
    }
}