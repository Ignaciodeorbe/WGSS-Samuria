using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    [SerializeField]
    int translationSpeed = 1;
    [SerializeField]
    float rotationSpeed = 0.25f;


    Vector3 translationVal = new Vector3(1.5f,-1,0);

    Vector3 spin = new Vector3(0,0,-.05f);

    public int TranslationSpeed { set { translationSpeed = value; } }
    
    public float RotationSpeed { set { rotationSpeed = value; } }


    // Update is called once per frame
    void Update()
    {
        MoveSword();   
    }

    /// <summary>
    /// Makes the sword move and rotate
    /// </summary>
    public void MoveSword()
    {
        //*Makes the sword move down
        //*Makes the sword rotate

        //Increases position by translation val which is going to the right and the down times the speed
        //Rotates the the z by spin and the speed.
        transform.position += translationVal * translationSpeed * Time.deltaTime;
        //transform.Rotate(spin * rotationSpeed * Time.deltaTime);
    }


    /// <summary>
    /// Resets the position of the sword
    /// </summary>
    public void ResetInfo()
    {
        transform.position = new Vector3(-13,2,0);
        transform.rotation = Quaternion.Euler(0,0,-33);
        rotationSpeed = 0.25f;
        translationSpeed = 1;
    } 
}
