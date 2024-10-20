using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] private Player player;

    [Space(5), Header("Setting")]
    public int point;
    private float displayPoint;
    [SerializeField] private int maxBomb;
    
    [Space(5), Header("UI")]
    [SerializeField] private GameObject levelUP;
    [SerializeField] private TextMeshPro pointText;
    [SerializeField] private TextMeshPro bombText;
    [SerializeField] private Image[] selectorImages;

    [Space(5), Header("Image")]
    [SerializeField] private Sprite[] bulletImages;

    [Space(5), Header("BulletText")]
    [SerializeField] private GameObject[] level1Text1;
    [SerializeField] private GameObject[] level2Text1;
    [SerializeField] private GameObject[] level3Text1;
    [SerializeField] private GameObject[] level1Text2;
    [SerializeField] private GameObject[] level2Text2;
    [SerializeField] private GameObject[] level3Text2;

    private int[] bulletLevel;
    private BulletType[] selectorType;
    private int bomb;

    void Start()
    {
        bulletLevel = player.bulletLevel;
        bomb = maxBomb;
    }

    void Update()
    {
        UpdatePoint();
        if(Input.GetKey(KeyCode.F1)) point += 10000;
    }
    private void UpdatePoint()
    {
        if(displayPoint < point) displayPoint = Mathf.Lerp(displayPoint, point, Time.deltaTime * 20);
        if(displayPoint + 1 > point) displayPoint = point;
        pointText.text = ((int)displayPoint).ToString("D7");
    }

    public void UsePoint()
    {
        displayPoint = point;
        pointText.text = ((int)displayPoint).ToString("D7");
    }

    public void GetPoint(int getValue)
    {
        point += getValue;
    }

    public bool UseBomb()
    {
        if(bomb <= 0) return false;
        bomb--;
        bombText.text = "BOMB : " + bomb;
        return true;
    }

    public void AddBomb()
    {
        bomb++;
        bombText.text = "BOMB : " + bomb;
    }
}
