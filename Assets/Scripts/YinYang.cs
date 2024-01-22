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

    void Start()
    {
        // Call RandomScale to scale the symbol randomly
        RandomScale();
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
}
