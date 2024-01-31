using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunchLeavesMovement : MonoBehaviour
{
    [SerializeField]
    private int speed;


    //Sets the position when the object is instantiated
    public void Awake()
    {
        transform.position = new Vector3(0, 0, 98);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    //Updates the position of the object
    void Update()
    {
        MoveLeaves();
    }

    //MoveCrow method moves crow across screen
    public void MoveLeaves()
    {
        //Calculate left boundary of the screen
        //float leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(40, 0, 0)).x;

        //Moves the leaves based on direction * the speed * the speed
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        //If the leaves go out of bounds
        if (transform.position.y <= -30)
        {
            //Destroys this gameObject
            Destroy(gameObject);
        }
    }
}
