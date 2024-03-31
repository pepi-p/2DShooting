using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] private Player player;
    [SerializeField] private BackToTitle backToTitle;

    [Space(5), Header("Setting")]
    [SerializeField] private float maxHP;

    [Space(5), Header("UI")]
    [SerializeField] private RectTransform hpBar;

    private float hp;
    public float HP
    {
        set
        {
            this.hp = Mathf.Clamp(value, 0, this.maxHP);
            this.hpBar.localScale = new Vector3(this.hp / this.maxHP, 1, 1);
            this.hpBar.transform.localPosition = new Vector3(-(1 - (this.hp / this.maxHP)) / 2, 0, -1);
            if(this.hp <= 0) backToTitle.EndGame();
        }
        get
        {
            return this.hp;
        }
    }

    private void Start()
    {
        HP = maxHP;
    }

    public void MaxHPUP(int value)
    {
        maxHP += value;
        HP += value;
    }
}
