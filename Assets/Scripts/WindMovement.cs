using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour
{
    [SerializeField]
    private int speed;


    //Sets the position when the object is instantiated
    public void Awake()
    {
        transform.position = new Vector3(0, 0, 100);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    //Updates the position of the object
    void Update()
    {
        MoveWind();
    }

    //MoveCrow method moves crow across screen
    public void MoveWind()
    {
        //Calculate left boundary of the screen
        //float leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(40, 0, 0)).x;

        //Moves the wind based on direction * the speed * the speed
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        //If the leaves go out of bounds
        if (transform.position.x <= -50)
        {
            //Destroys this gameObject
            Destroy(gameObject);
        }
    }
}
