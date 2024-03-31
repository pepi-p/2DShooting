using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField] private GameObject[] horizontalBar;
    [SerializeField] private GameObject[] verticalBar;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        for(int i = 0; i < horizontalBar.Length; i++)
        {
            horizontalBar[i].transform.localPosition += new Vector3(0, horizontalSpeed * Time.deltaTime, 0);
            if(Mathf.Abs(horizontalBar[i].transform.localPosition.y) > 5) horizontalBar[i].transform.localPosition = new Vector3(0, -5 * Mathf.Sign(horizontalSpeed), 0);
        }
        for(int i = 0; i < verticalBar.Length; i++)
        {
            verticalBar[i].transform.localPosition += new Vector3(verticalSpeed * Time.deltaTime, 0, 0);
            if(Mathf.Abs(verticalBar[i].transform.localPosition.x) > 5) verticalBar[i].transform.localPosition = new Vector3(-5 * Mathf.Sign(verticalSpeed), 0, 0);
        }
    }
}
