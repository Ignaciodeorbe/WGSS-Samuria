using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer srender_hands;

    [SerializeField]
    private SpriteRenderer srender_sword;


    [SerializeField]
    private SpriteRenderer srender_yinyang;

    [SerializeField]
    SwordMovement movement;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        srender_sword.color = Color.red;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.TranslationSpeed = 0;
            movement.RotationSpeed = 0;
            srender_hands.enabled = true;
            srender_yinyang.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        srender_sword.color = Color.white;
    }
}

    
