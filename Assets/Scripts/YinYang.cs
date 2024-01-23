//Created by Owen Beck
//Handles yinyang spinning and resizing

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YinYang : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 50f; //Rotation speed of Yin and Yang symbol

    [SerializeField]
    Vector2 scaleBounds = new Vector2(0.5f, 2f); // Bounds for random scale

    [SerializeField]
    Vector2 posXBounds = new Vector2(0.5f, 2f); // Bounds for random x position
    [SerializeField]
    Vector2 posYBounds = new Vector2(0.5f, 2f); // Bounds for random x position

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
        float randomScale = Random.Range(scaleBounds.x, scaleBounds.y);

        // Set the local scale of the object to the random scale
        transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }

    //Function to randomly move the yin yang symbol each round within certain bounds
    public void RandomPosition()
    {
        // Generate a random x position within the specified bounds on the right side
        float randomXPos = Random.Range(posXBounds.x, posXBounds.y);
        float randomYPos = Random.Range(posYBounds.x, posYBounds.y);

        // Set the position of the object to the random position on the right side
        transform.position = new Vector3(randomXPos, randomYPos, transform.position.z);
    }
}
