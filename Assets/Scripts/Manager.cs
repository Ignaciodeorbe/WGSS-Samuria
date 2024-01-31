using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Manager : MonoBehaviour
{

    States currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = States.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case States.Idle:

                break;


            case States.SwordClapped:

                break;


            case States.Dead:

                break;

        }

    }
}
