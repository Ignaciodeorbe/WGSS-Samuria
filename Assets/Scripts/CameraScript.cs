using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Quits the game if escape is clicked
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }
}
