//Made by Jake Wardell
//Handles collision between sword and yin yang

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private YinYang symbol;

    [SerializeField]
    private SpriteRenderer srender_hands;

    [SerializeField]
    private SpriteRenderer srender_severedHands;

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

    [SerializeField]
    GameObject bloodSplatter; //Owen Addition

    [SerializeField]
    Tutorial tutorialManager; // Ignacio Addition-- for determining if tutorial is active



    public bool CanCatch { set { canCatch = value; }  get { return canCatch; } }

    [SerializeField]
    SwordMovement movement;

    // Update is called once per frame
    void Update()
    {
        if (bColi2d_sword.IsTouching(cirColi2d_yinyang) && Input.GetKeyDown(KeyCode.Space) && canCatch)
        {
            movement.CanMove = false;
            srender_hands.enabled = true;
            srender_yinyang.enabled = false;
            canCatch = false;
            srender_sword.color= Color.white;
            manager.CurrentState = States.SwordClapped;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && tutorialManager == null)
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
            //Makes it so only if you have clapped will the severed hands appear.
            if (canCatch == false)
            {
                srender_severedHands.enabled = true; //Could use some optimization but I'm leaving this here for now -Owen
                bloodSplatter.transform.position = new Vector3(6.5f, -1.45f, 99);
            }
            else
            {
                bloodSplatter.transform.position = new Vector3 (8, -2, 99);
            }
            srender_hands.enabled = false;
            bloodSplatter.SetActive(true);  //Show blood

            manager.CurrentState = States.Dead;
        }
        
        
    }

    public void ResetCollisionInfo()
    {
        srender_hands.enabled = false;
        srender_severedHands.enabled = false;
        srender_yinyang.enabled = true;
        canCatch = true;
        symbol.RandomPosition(); //Moves position of the symbol each round
    }

    /// <summary>
    /// Reset specifically for the tutorial, doesn't include the random location moving
    /// </summary>
    public void ResetTutorialCollisionInfo()
    {
        srender_hands.enabled = false;
        srender_severedHands.enabled = false;
        bloodSplatter.SetActive(false);
        srender_yinyang.enabled = true;
        canCatch = true;
    }
}

    
