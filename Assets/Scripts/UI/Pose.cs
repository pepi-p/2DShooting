using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pose : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject pose;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject techTree;
    [SerializeField] private GameObject status;
    [SerializeField] private TextMeshProUGUI pointText;

    private bool isStop = false;
    private bool isOpenUI = false;

    private void Start()
    {
        pose.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpenUI)
            {
                isOpenUI = false;
                buttons.SetActive(true);
                techTree.SetActive(false);
                status.SetActive(false);
            }
            else
            {
                isStop ^= true;
                pose.SetActive(isStop);
                UpdatePointDisplay();
            }
            Time.timeScale = isStop ? 0 : 1;
        }
    }

    public void OpenTechTree()
    {
        isOpenUI = true;
        buttons.SetActive(false);
        techTree.SetActive(true);
    }

    public void OpenStatus()
    {
        isOpenUI = true;
        buttons.SetActive(false);
        status.SetActive(true);
    }

    public void UpdatePointDisplay()
    {
        pointText.text = "所持Point : " + levelManager.point;
    }
}
