using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tutorial : GameplayManager
{
    [SerializeField]
    List<GameObject> tutorialText = new List<GameObject>();
    int textIndex = 0;

    [SerializeField]
    protected float tutorialTimer = 0.4f;


    // Start is called before the first frame update
    void Start()
    {       
        curve = 7;
        currentState = States.Tutorial;

        tutorialText.Add(gameObject);
        tutorialText.Add(gameObject);
        tutorialText.Add(gameObject);


    }

    // Update is called once per frame
    void Update()
    {
     


        switch (currentState)
        {
            case States.Tutorial:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    textIndex++;

                    // Continues the tutorial
                    if(textIndex >= 3) // <---- switch 3 to tutorialtext.count
                    {
                        NextLevel();
                    }

                    // Shows current dialogue box
                    else
                    {
                        //ShowCurrentText();
                    }
                }
                
               

                break;

            case States.Idle:
                if (started)
                {
                    soundTimer -= Time.deltaTime;

                    if (soundTimer < 0)
                    {
                        tutorialTimer -= Time.deltaTime;

                        audioSource.PlayOneShot(scream);

                        // Stops the sword for a text box to be displayed
                        if(tutorialTimer < 0)
                        {
                            swordMovementScript.CanMove = false;

                            // Add text box here and black over lay  -------------------------------------------------

                            // When the user presses space the tutorial will continue
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                tutorialTimer = 10000f;
                            }

                        }
                        else
                        {
                        swordMovementScript.CanMove = true;

                        }
                        audioSource.clip = null;

                      // if (soundCounter < gameObjectList.Count)
                      // {
                      //     Debug.Log("I was called!");
                      //     Instantiate(gameObjectList[soundCounter], new Vector3(-30, 0, 0), Quaternion.identity);
                      // }
                      // if (soundCounter == gameplaySounds.Count - 1)
                      // {
                      // }
                      // else
                      // {
                      //     soundCounter++;
                      //     //audioSource.clip = gameplaySounds[soundCounter];
                      //     SetTimer();
                      // }
                    }

                }
                idleTimer = 1.4f;
                break;


            case States.SwordClapped:
                idleTimer -= Time.deltaTime;


                if (idleTimer < 0)
                {
                    swordCaught.Invoke();
                    SceneManager.LoadScene("GameScene");

                    //NextLevel();
                }
                break;


            case States.Dead:
                idleTimer -= Time.deltaTime;

                if (idleTimer < 0)
                {
                    swordCaught.Invoke();
                    NextLevel();
                }
                break;

        }

    }

    public void SetTimer()
    {
        soundTimer = timerScale;
        soundTimer += Random.Range(0.5f, 0.8f);
    }

    public void NextLevel()
    {

        MakeSounds();
        SetTimer();
        started = true;

        
        currentState = States.Idle;
    }

    public void MakeSounds()
    {
        soundCounter = 0;
        gameplaySounds.Clear();
        gameObjectList.Clear();
        int randVal = Random.Range(0, listSounds.Count);

        for (int i = 0; i < Random.Range(0, 3); i++)
        {
            gameplaySounds.Add(listSounds[randVal]);
            gameObjectList.Add(ListObjects[randVal]);
        }

        gameplaySounds.Add(scream);

        audioSource.clip = gameplaySounds[soundCounter];


    }

    /// <summary>
    /// Displays the current dialogue box
    /// </summary>
    public void ShowCurrentText()
    {
        for (int i = 0; i < tutorialText.Count; i++)
        {
            if (i == textIndex)
            {
                // Show the current dialogue box
                tutorialText[i].SetActive(true);
            }
            else
            {
                // Hide other dialogue boxes
                tutorialText[i].SetActive(false);
            }
        }
    }
 
}
