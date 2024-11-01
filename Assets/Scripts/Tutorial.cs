using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject startText;

    public delegate void StageStart();
    public StageStart stageStart;

    void Start()
    {
        StartCoroutine(StartTextFlash());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) levelManager.AddBomb();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stageStart.Invoke();
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator StartTextFlash()
    {
        while(true)
        {
            startText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            startText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
