using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BackToTitle : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject endUI;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TMP_InputField inputField;
    public void EndGame()
    {
        Time.timeScale = 0;
        waveText.text = "到達wave : " + stageManager.wave.ToString();
        endUI.SetActive(true);
    }

    public void EnterName()
    {
        Ranking.names[5] = inputField.text;
        Ranking.waves[5] = stageManager.wave;
    }

    public void LoadTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
