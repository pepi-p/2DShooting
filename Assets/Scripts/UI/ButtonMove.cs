using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonMove : MonoBehaviour
{
    [SerializeField] private ButtonType type;
    private Animator animator;

    private enum ButtonType
    {
        StartGame,
        Description,
        Confing,
        Exit
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Enter()
    {
        animator.SetBool("isPointer", true);
    }

    public void Exit()
    {
        animator.SetBool("isPointer", false);
    }

    public void Click()
    {
        animator.SetTrigger("isClick");
    }

    public void SelectStart()
    {
        switch(type)
        {
            case ButtonType.StartGame:
                SceneManager.LoadScene("Stage1");
                break;
            case ButtonType.Description:
                break;
            case ButtonType.Confing:
                SceneManager.LoadScene("Menu");
                break;
            case ButtonType.Exit:
                Application.Quit();
                break;
        }
    }
}
