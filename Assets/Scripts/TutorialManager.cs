using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tutorial : GameplayManager
{

    List<bool> tutorialText = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {       
        curve = 7;
        currentState = States.Tutorial;

        for(int i = 0; i < 4; i++)
        {
            tutorialText.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
     


        switch (currentState)
        {
            case States.Tutorial:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialText[0] = true;

                }
                
                if (Input.GetKeyDown(KeyCode.Space) || tutorialText[0])
                {
                    tutorialText[1] = true;

                    if (Input.GetKeyDown(KeyCode.Space) || tutorialText[1])
                    {
                        NextLevel();
                        tutorialText[3] = true;


                    }
                }

                break;

            case States.Idle:
                if (started)
                {
                    soundTimer -= Time.deltaTime;

                    if (soundTimer < 0)
                    {


                        audioSource.PlayOneShot(scream);
                        swordMovementScript.CanMove = true;
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
 
}
