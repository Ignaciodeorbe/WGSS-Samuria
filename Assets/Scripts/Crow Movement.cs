using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject crow;

    [SerializeField]
    private int speed;

    private Vector3 dir = Vector3.left;


    void Update()
    {
        MoveCrow();
    }

     //MoveCrow method moves crow across screen
     public void MoveCrow()
    {
        //Calculate left boundary of the screen
        float leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(-10, 0, 0)).x;

        transform.Translate(dir * speed * Time.deltaTime);

        if (transform.position.x <= leftBoundary)
        {
            dir = Vector3.right;
        }
        else if (transform.position.x >= leftBoundary)
        {
            dir = Vector3.left;
        }
    }
}
