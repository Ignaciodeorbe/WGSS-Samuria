//Created by: Owen Beck & Jake Wardell
//Moves the group of crows.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    [SerializeField]
    private int speed;


    //Sets the position when the object is instantiated
    public void Awake()
    {
        transform.position = new Vector3(-20,0,25);
        transform.rotation = new Quaternion(0, 180, 0,0);
    }

    //Updates the position of the object
    void Update()
    {
        MoveCrow();
    }

     //MoveCrow method moves crow across screen
     public void MoveCrow()
    {
        //Calculate left boundary of the screen
        //float leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(40, 0, 0)).x;

        //Moves the crows based on direction * the speed * the speed
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        //If the birds go out of bounds
        if (transform.position.x >= 16)
        {
            //Destroys this gameObject
            Destroy(gameObject);
        }
    }
}
