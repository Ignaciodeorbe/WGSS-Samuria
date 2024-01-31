//Created by Jake Wardell
//Handels sword movement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    //Scalers for both the speed of the sword movement and the rotation of the sword
    [SerializeField]
    int translationSpeed = 1;
    [SerializeField]
    float rotationSpeed = 0.25f;

    //If the sword can move or not
    private bool canMove = false;

    //Starting pos
    Vector3 startPos;

    //THe values to translate
    Vector3 translationVal = new Vector3(1.5f,-1,0);

    //THe spin value
    Vector3 spin = new Vector3(0,0,-1f);

    //Gets and sets the translation speed of the sword
    public int TranslationSpeed { get { return translationSpeed; } set { translationSpeed = value; } }
    


    //Sets canMove bool
    public bool CanMove { set { canMove = value;} }

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveSword();
        }
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
        transform.Rotate(spin * rotationSpeed * Time.deltaTime);
    }


    /// <summary>
    /// Resets the position of the sword and rotation
    /// </summary>
    public void ResetInfo()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0,0,-33);
        
    } 
}
