using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyPathMove : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 6f;
    
    private Vector3 endPos;
    private float moveDistance;

    void Start()
    {
        endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
    }

    void Update()
    {
        moveDistance += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop) * Quaternion.Euler(0, 90, 0);
        if(Vector3.Distance(transform.position, endPos) < 0.01f) Destroy(this.gameObject);
    }
}
