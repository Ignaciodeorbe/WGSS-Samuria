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
    float tutorialTimer = 0.4f;

    [SerializeField]
    SpriteRenderer textBox;

    [SerializeField]
    SpriteRenderer bgFade;

    [SerializeField]
    GameObject spaceToContinueText;

    [SerializeField]
    GameObject finalText;

    [SerializeField]
    int tutorialCounter = 3;

    [SerializeField]
    GameObject extraTutorialText;


    // Start is called before the first frame update
    void Start()
    {       
        curve = 7;
        currentState = States.Tutorial;        

        bgFade.enabled = false;
        finalText.SetActive(false);




    }

    // Update is called once per frame
    void Update()
    {
     


        switch (currentState)
        {
            case States.Tutorial:
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    textIndex++;

                    // Continues the tutorial
                    if(textIndex >= tutorialText.Count) 
                    {
                        textBox.enabled = false;
                        spaceToContinueText.SetActive(false);

                        ShowCurrentText();

                        NextLevel();
                    }

                    // Shows current dialogue box
                    else
                    {
                        ShowCurrentText();
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
                        scream = null; //Jake added this it stops the ear rape not the best solution

                        // Stops the sword for a text box to be displayed
                        if(tutorialTimer < 0)
                        {
                            swordMovementScript.CanMove = false;
                            
                            // Enables tutorial items
                            textBox.enabled = true;
                            bgFade.enabled = true;
                            spaceToContinueText.SetActive(true);
                            finalText.SetActive(true);


                            // When the user presses space the tutorial will continue
                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                            {
                                // Disables tutorial items
                                tutorialTimer = 10000f;
                                bgFade.enabled = false;
                                textBox.enabled = false;
                                spaceToContinueText.SetActive(false);
                                finalText.SetActive(false);

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
                }
                break;


            case States.Dead:
                idleTimer -= Time.deltaTime;

                // Display text if they miss 4 or more times
                if (tutorialCounter <= 0)
                {
                    textBox.enabled = true;
                    extraTutorialText.SetActive(true);
                }

                if (idleTimer < 0)
                {
                    swordCaught.Invoke();
                    tutorialCounter--;
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
