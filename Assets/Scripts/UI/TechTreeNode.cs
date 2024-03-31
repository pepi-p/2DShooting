using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TechTreeNode : MonoBehaviour
{
    [SerializeField] private TechTreeManager techTreeManager;
    [SerializeField] private TextMeshProUGUI nodeName;
    [SerializeField] private Image unlockImage;
    [SerializeField] private Image lockedImage;
    [SerializeField] private Image[] nextEdge;

    public void UnlockNode()
    {
        unlockImage.color = techTreeManager.unlockedColor;
        for(int i = 0; i < nextEdge.Length; i++) nextEdge[i].color = techTreeManager.enableEdgeColor;
    }

    public void EnableNode()
    {
        lockedImage.enabled = false;
        unlockImage.enabled = true;
        unlockImage.color = techTreeManager.unlockAbleColor;
        nodeName.color = techTreeManager.enableTextColor;
        GetComponent<Button>().enabled = true;
    }

    public void UnlockEdge()
    {
        for(int i = 0; i < nextEdge.Length; i++) nextEdge[i].color = techTreeManager.unlockedEdgeColor;
    }
}