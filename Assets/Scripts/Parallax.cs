using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Variables
    private float length;
    private float startPos;

    [SerializeField]
    public float parallaxValue;
    [SerializeField]
    public float parallaxScale;

    float distance = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Sets the starting position and gets the size of the background that will be the parallax effect
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculates the distance the background will move
        distance += (parallaxScale * parallaxValue);
     
        // Moves the background
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // When the background segment is off the screen then it is moved back so it can continue the parallax effect
        if(distance > length)
        {           
            distance -= length;
        }
    }
}
