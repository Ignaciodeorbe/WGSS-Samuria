//Created by Owen Beck
//Handles yinyang spinning and resizing

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YinYang : MonoBehaviour
{
    [SerializeField]
    GameplayManager gameplay;

    [SerializeField]
    float rotationSpeed = 50f; //Rotation speed of Yin and Yang symbol

    [SerializeField]
    Vector2 scaleBounds = new Vector2(0.5f, 2f); // Bounds for random scale

    /*
    [SerializeField]
    Vector2 posXBounds = new Vector2(0.5f, 2f); // Bounds for random x position
    [SerializeField]
    Vector2 posYBounds = new Vector2(0.5f, 2f); // Bounds for random x position
    */

    Vector2 posXBounds = new Vector2(4, 6); // Bounds for random x position
    
    Vector2 posYBounds = new Vector2(-1.4f, -3); // Bounds for random x position

    //Range is X: 4 to 6.3
    //Range is Y: -1.4 to -3

    void Start()
    {
        // Call RandomScale to scale the symbol randomly
        RandomScale();

        // Call RandomPosition to set a random position
        RandomPosition();
    }

    void Update()
    {
        // Rotate the symbol around the Z-axis to give spinning effect
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

    }

    //Function to change size of the YinYang symbol each time game is refreshed
    public void RandomScale()
    {
        // Generate a random scale within the specified bounds
        float randomScale;
        //randomScale = Random.Range(scaleBounds.x, scaleBounds.y);

        if (gameplay.Curve - 5 <= 0)
        {
            //Scale the yin yang larger
            randomScale = Random.Range(0.19f, 0.25f);
        }
        else if (gameplay.Curve - 5 >= 1 && gameplay.Curve - 5 <= 5)
        {
            //Scale the yin yang medium
            randomScale = Random.Range(0.12f, 0.19f);
        }
        else
        {
            //Scale the yin yang smaller
            randomScale = Random.Range(0.01f, 0.12f);
        }

        // Set the local scale of the object to the random scale
        transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }

    //Function to randomly move the yin yang symbol each round within certain bounds
    public void RandomPosition()
    {
        // Generate a random x position within the specified bounds on the right side
        
        float randomXPos = Random.Range(posXBounds.x, posXBounds.y);
        float randomYPos = Random.Range(posYBounds.x, posYBounds.y);
        
        /*
        float randomXPos = Random.Range(4, 6.3f);
        float randomYPos = Random.Range(-1.4f, -3);
        */

        // Set the position of the object to the random position on the right side
        transform.position = new Vector3(randomXPos, randomYPos, transform.position.z);
    }
}
