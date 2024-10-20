using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Stage stage;
    [SerializeField] private GameObject startText;

    void Start()
    {
        StartCoroutine(StartTextFlash());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) levelManager.AddBomb();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stage.Init();
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
