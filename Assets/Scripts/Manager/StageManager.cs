using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TextMeshPro waveText;
    public int wave = 0;

    private void Start()
    {
        waveText.text = "WAVE : 1";
    }

    public void NextWave()
    {
        wave++;
        waveText.text = "WAVE : " + wave.ToString();
        levelManager.GetPoint(500);
        if(wave % 6 == 1 && wave > 1)
        {
            player.damageMultiply *= 0.2f;
            levelManager.GetPoint(500 * (wave / 6));
        }
    }
}
