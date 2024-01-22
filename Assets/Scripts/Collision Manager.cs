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
    private GameObject go_sword;

    [SerializeField]
    private GameplayManager manager;


    [SerializeField]
    private SpriteRenderer srender_yinyang;

    bool canCatch = true;

    

    public bool CanCatch { set { canCatch = value; } }

    [SerializeField]
    SwordMovement movement;
    // Update is called once per frame
    void Update()
    {
        if (bColi2d_sword.IsTouching(cirColi2d_yinyang) && Input.GetKeyDown(KeyCode.Space) && canCatch)
        {
            movement.TranslationSpeed = 0;
            movement.RotationSpeed = 0;
            srender_hands.enabled = true;
            srender_yinyang.enabled = false;
            canCatch = false;
            srender_sword.color= Color.white;
            manager.CurrentState = States.SwordClapped;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            srender_hands.enabled = true;
            srender_yinyang.enabled = false;
            canCatch= false;
        }

        if(!bColi2d_sword.IsTouching(cirColi2d_yinyang) && srender_sword.color == Color.red)
        {
            srender_sword.color = Color.white;
        }else if (bColi2d_sword.IsTouching(cirColi2d_yinyang) && srender_sword.color == Color.white && canCatch)
        {
            srender_sword.color = Color.red;
        }

        if(go_sword.transform.position.x > 7 && go_sword.transform.position.y < -10) {
            manager.CurrentState = States.Dead;
        }
        
        if(manager.CurrentState == States.Idle && srender_hands.enabled == true && srender_yinyang.enabled == false && canCatch == true)
        {
            srender_hands.enabled = false;
            srender_yinyang.enabled = true;
        }
    }
}

    
