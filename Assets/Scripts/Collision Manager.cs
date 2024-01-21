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
    private BoxCollider2D bColi2d_sword;

    [SerializeField]
    private CircleCollider2D cirColi2d_yinyang;


    [SerializeField]
    private SpriteRenderer srender_yinyang;

    [SerializeField]
    SwordMovement movement;
    // Update is called once per frame
    void Update()
    {
        if (bColi2d_sword.IsTouching(cirColi2d_yinyang) && Input.GetKeyDown(KeyCode.Space))
        {
            movement.TranslationSpeed = 0;
            movement.RotationSpeed = 0;
            srender_hands.enabled = true;
            srender_yinyang.enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("miss");
        }

        if(!bColi2d_sword.IsTouching(cirColi2d_yinyang) && srender_sword.color == Color.red)
        {
            srender_sword.color = Color.white;
        }else if (bColi2d_sword.IsTouching(cirColi2d_yinyang) && srender_sword.color == Color.white)
        {
            srender_sword.color = Color.red;
        }
    }
}

    
